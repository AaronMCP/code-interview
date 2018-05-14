import * as React from 'react';
import { Modal, Table, Button, Icon, Input, Form, Select, Checkbox, DatePicker, message } from 'antd';
import moment from 'moment';
import { TableDataStore, TableDescriptionStore } from '../../store';
import { ITableData } from './interface';
import { observer } from 'mobx-react';
import { getJSON, readExcel, readFile } from '../common/import/import-excel';
import { observable, action, toJS, runInAction } from 'mobx';
import zh_CN from 'antd/lib/date-picker/locale/zh_CN';
import 'moment/locale/zh-cn';
moment.locale('zh-cn');
import './style.scss';

const tableDataStore = new TableDataStore();
const tableDescriptionStore = new TableDescriptionStore();
const FormItem = Form.Item;
const { TextArea } = Input;
const formLayout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 16 }
};
const Option = Select.Option;

@observer
export class TableData extends React.Component<ITableData, {}> {
    public columns: Array<any>;
    public defaultSearchColums: Array<any>;
    public primaryKey: string;
    public width: number;
    public scrollX: number;
    public searchFlag: boolean;
    // 图表数据
    @observable public pagination: any;
    @observable public paginationModel: any;
    public selectedValue: string;
    public loading: boolean;
    @observable public searchModalFlag: boolean;
    @observable public editModalFlag: boolean;
    public addFlag: boolean;
    @observable public searchDataFlag: boolean;
    @observable public searchConditions: object;
    @observable public selectedData: Array<any>;
    @observable public selectedRowKeys: Array<any>;
    @observable public updateData: any;
    @observable public searchValue: string;
    @observable public primaryFlag: boolean;
    @observable private previewShow: boolean = false;
    @observable private importFrom: boolean = false;
    /**
     * 错误的列
     */
    @observable private errRows: any[] = [];
    @observable private previewData: any = [];
    private fileInput: any;
    constructor(props: ITableData) {
        super(props);
        this.columns = [];
        this.primaryKey = '';
        this.defaultSearchColums = [];
        this.selectedValue = '';
        this.searchFlag = false;
        this.width = 150;
        this.scrollX = 0;
        this.searchModalFlag = false;
        this.editModalFlag = false;
        this.addFlag = false;
        this.loading = false;
        this.searchConditions = {};
        this.pagination = {
            current: 1,
            pageSize: 20,
            total: this.props.total
        };
        this.paginationModel = {
            current: 1,
            pageSize: 20,
            total: 0
        };
        this.selectedRowKeys = [];
        this.selectedData = [];
        this.updateData = {};
        this.primaryFlag = false;

    }
    // 修改数据赋空值
    public getEmptyData = () => {
        this.updateData = {};
        this.props.columns.forEach(item => {
            if (item.dataType === 'bit') {
                this.updateData[item.code] = false;
            } else if (item.dataType === 'dateTime') {
                this.updateData[item.code] = null;
            } else {
                this.updateData[item.code] = '';
            }
        });
    }

    // columns
    public getColumns = () => {
        this.columns = [];
        this.defaultSearchColums = [];
        this.props.columns.forEach((item, index) => {
            if (item.code !== '') {
                if (item.dataType.toLowerCase() !== 'bit') {
                    this.columns.push({
                        'title': item.name,
                        'dataIndex': item.code,
                        'width': this.width.toString() + 'px'
                    });
                } else {
                    this.columns.push({
                        'title': item.name,
                        'dataIndex': item.code,
                        'width': this.width.toString() + 'px',
                        render: (text: any, record: any, index: number) => {
                            return text ? '是' : '否';
                        }
                    });
                }
            }
            // 默认搜索条件
            if (item.isDefaultSearch) {
                this.defaultSearchColums.push({
                    'name': item.name,
                    'code': item.code
                });
            }

            // 主键
            if (item.isPrimary) {
                this.primaryKey = item.code;
            }
        });
        this.scrollX = this.width * this.props.columns.length;
    }

    // public shouldComponentUpdate(nextProps: any, nextState: any): boolean {
    //     if (nextProps.flag) {
    //         return false;
    //     }
    //     return true;
    // }

    public componentWillReceiveProps(nextProps: any): void {
        if (this.props.tableId !== nextProps.tableId) {
            this.searchDataFlag = false;
        }
        this.pagination.total = nextProps.total;
        this.selectedData = [];
        this.selectedRowKeys = [];
    }

    public render(): JSX.Element {
        this.getColumns();
        const rowSelection = {
            onChange: (selectedRowKeys, selectedRows) => {
                this.selectedData = selectedRows;
                this.selectedRowKeys = selectedRowKeys;
            },
            selectedRowKeys: this.selectedRowKeys.slice(0),
            type: ('checkbox' as 'checkbox' | 'radio')
        };
        return (
            <div className='table-data'>
                <form id='import-form' action=''>
                    <input accept='.xlsx, .xls' onChange={this.preview}
                        ref={ref => (this.fileInput = ref)} type='file' />
                </form>
                <Modal
                    title='预览'
                    cancelText='取消'
                    okText='导入'
                    onCancel={this.previewchange}
                    onOk={this.import}
                    className='preview-modal'
                    visible={this.previewShow}
                    width='940px'
                >
                    <Table
                        className={'preview'}
                        columns={toJS(this.columns)}
                        dataSource={toJS(this.previewData)}
                        pagination={this.paginationModel}
                        onChange={this.previewTableChange}
                        rowKey={(record: any) => { return record[this.primaryKey]; }}
                        scroll={{ x: this.scrollX, y: 330 }}
                    />
                    <span className='err-message'>
                        {`读取数据${this.previewData.length}条`}
                    </span>
                </Modal>
                <Table
                    rowSelection={rowSelection}
                    onRowClick={this.rowClick}
                    columns={this.columns}
                    rowKey={(record: any) => { return record[this.primaryKey]; }}
                    dataSource={toJS(this.props.data)}
                    pagination={this.pagination}
                    loading={this.loading}
                    onChange={this.tableChange}
                    scroll={{ x: this.scrollX, y: 330 }}
                    rowClassName={this.rowClassName}
                ></Table>
                {
                    this.getEditModal()
                }
                {
                    this.getSearchModal()
                }

                <div className='table-operate'>
                    {
                        this.searchDataFlag
                            ?
                            <div className='operate-data operate-container'>
                                <div className='common-data-operate' onClick={this.searchModalShow}>
                                    <div className='search-button'>高级搜索</div>
                                </div>
                                <div className='common-data-operate'>
                                    <Button icon='close' onClick={() => { this.searchDataFlag = false; }}></Button>
                                </div>
                                <div className='common-data-operate'>
                                    <Select
                                        onChange={(value) => this.searchChange(value)}
                                    >
                                        {
                                            this.defaultSearchColums.map(item => {
                                                return <Option key={item.name}>{item.name}</Option>;
                                            })
                                        }
                                    </Select>
                                </div>
                                <div className='common-data-operate'>
                                    <Input className='input-search' value={this.searchValue}
                                        onChange={(e) => { this.searchInputValue(e); }} />
                                </div>
                                <div className='common-data-operate search'
                                    onClick={this.searchDefaultData}>搜索</div>
                            </div>
                            :

                            <div className='operate-constructor operate-container'>
                                <div className='common-operate' onClick={this.startData}>
                                    <div className='common-operate-background'></div>
                                    <span>启用</span>
                                </div>
                                <div className='common-operate operate-stop' onClick={this.stopData}>
                                    <div className='common-operate-background operate-stop-background'></div>
                                    <span>停用</span>
                                </div>
                                <div className='common-operate' onClick={this.importData}>
                                    <div className='common-operate-background'></div>
                                    <span>导入</span>
                                </div>
                                <div className='common-operate' onClick={this.addData}>
                                    <div className='common-operate-background'></div>
                                    <span>添加</span>
                                </div>
                                <div className='common-operate' onClick={this.updateTableData}>
                                    <div className='common-operate-background'></div>
                                    <span>修改</span>
                                </div>
                                <div className='common-operate operate-search' onClick={this.searchDataShow}>
                                    <div className='common-operate-background'></div>
                                    <span>搜索</span>
                                </div>
                            </div>

                    }
                </div>`
            </div>
        );
    }

    // 点击选中行
    @action
    public rowClick = (record: any, index: number, event: any) => {
        let keyFlag = false;
        let keyIndex = -1;
        // 查看是否已被选中
        this.selectedRowKeys.forEach((item, ind) => {
            if (item === record[this.primaryKey]) {
                keyFlag = true;
                keyIndex = ind;
            }
        });
        if (keyFlag) {
            this.selectedRowKeys.splice(keyIndex, 1);
            this.selectedData.splice(keyIndex, 1);
        } else {
            this.selectedRowKeys.push(record[this.primaryKey]);
            this.selectedData.push(record);
        }
    }

    // 高级搜索modal
    public getSearchModal = () => {
        return (
            <Modal
                width={376}
                visible={this.searchModalFlag}
                title='高级搜索'
                onOk={this.searchOk}
                onCancel={this.searchCancel}
                cancelText='取消'
                okText='确定'>
                <Form className='search-modal-form'>
                    {
                        // tslint:disable-next-line:cyclomatic-complexity
                        this.props.columns.map((item) => {
                            if (item.searchable) {
                                const dataType = item.dataType.toLowerCase();
                                if (dataType === 'bit') {
                                    return <FormItem {...formLayout} label={item.name} key={item.code}>
                                        <Checkbox checked={this.searchConditions[item.code]}
                                            onChange={(e) => this.searchCheckChange(item.code, e)} />
                                    </FormItem>;
                                } else if (dataType === 'datetime') {
                                    let momentDate = null;
                                    if (this.searchConditions[item.code]) {
                                        momentDate = moment(this.searchConditions[item.code]);
                                    }
                                    return <FormItem {...formLayout} label={item.name} key={item.code}>
                                        <DatePicker
                                            locale={zh_CN}
                                            onChange={
                                                (date, datestring) => this.searchDateChange(date, datestring, item.code)
                                            }
                                            value={momentDate} />
                                    </FormItem>;
                                    // tslint:disable-next-line:max-line-length
                                } else if (dataType === 'int' || item.dataType === 'bigint' || item.dataType === 'smallint' || item.dataType === 'tinyint' || item.dataType === 'float') {
                                    return <FormItem {...formLayout} label={item.name} key={item.code}>
                                        <Input value={this.searchConditions[item.code]} type='number'
                                            onChange={(e) => this.searchInputChange(item.code, e)} />
                                    </FormItem>;
                                } else {
                                    return <FormItem {...formLayout} label={item.name} key={item.code}>
                                        <Input value={this.searchConditions[item.code]}
                                            onChange={(e) => this.searchInputChange(item.code, e)} />
                                    </FormItem>;
                                }
                            }
                        })
                    }
                </Form>
            </Modal>
        );
    }

    // 修改数据modal
    public getEditModal = () => {
        let modalText = '';
        if (this.addFlag) {
            modalText = '添加数据';
        } else {
            modalText = '修改数据';
        }
        return (
            <Modal
                width={700}
                visible={this.editModalFlag}
                title={modalText}
                onOk={this.editOk}
                onCancel={this.editCancel}
                cancelText='取消'
                okText='确定'>
                <Form className='edit-modal-form'>
                    {
                        // tslint:disable-next-line:cyclomatic-complexity
                        this.props.columns.map((item) => {
                            let labelName = item.nullable ? item.name : `* ${item.name}`;
                            let className = item.nullable ? '' : 'table-form-required';
                            const dataType = item.dataType.toLowerCase();
                            if (dataType === 'bit') {
                                const checked = this.updateData[item.code] ? true : false;
                                return <FormItem className={className} {...formLayout} label={labelName}
                                    key={item.code}>
                                    <Checkbox checked={checked}
                                        onChange={(e) => this.editCheckChange(item.code, e)} />
                                </FormItem>;
                            } else if (dataType === 'datetime') {
                                let momentDate = null;
                                if (this.updateData[item.code]) {
                                    momentDate = moment(this.updateData[item.code]);
                                }
                                return <FormItem className={className} {...formLayout} label={labelName}
                                    key={item.code}>
                                    <DatePicker
                                        locale={zh_CN}
                                        onChange={
                                            (date, datestring) => this.editDateChange(date, datestring, item.code)
                                        }
                                        value={momentDate} />
                                </FormItem>;
                            } else {
                                let flag = false;
                                if (item.isPrimary) {
                                    flag = true;
                                }
                                // tslint:disable-next-line:max-line-length
                                if (dataType === 'int' || dataType === 'bigint' || dataType === 'smallint' || dataType === 'tinyint' || dataType === 'float') {
                                    return <FormItem className={className} {...formLayout} label={labelName}
                                        key={item.code}>
                                        <Input value={this.updateData[item.code]}
                                            type='number'
                                            disabled={this.primaryFlag && flag}
                                            onChange={(e) => this.editInputChange(item.code, e)} />
                                    </FormItem>;
                                } else {
                                    return <FormItem className={className} {...formLayout} label={labelName}
                                        key={item.code}>
                                        <Input value={this.updateData[item.code]}
                                            disabled={this.primaryFlag && flag}
                                            onChange={(e) => this.editInputChange(item.code, e)} />
                                    </FormItem>;
                                }
                            }
                        })
                    }
                </Form>
            </Modal>
        );
    }

    public getFormInput = (labelName: string, props: string) => {
        return <FormItem {...formLayout} label={labelName}>
            <Input value={this.props.columns[props]} onChange={(e) => this.searchInputChange(props, e)} />
        </FormItem>;
    }

    // 操作表
    public tableChange = (pagination: any) => {
        this.pagination.current = pagination.current;
        this.searchData();
    }
    public previewTableChange = (paginationModel: any) => {
        let cu = paginationModel.current;
        this.forceUpdate();
    }
    // input函数
    public searchInputChange = (props: string, e: any) => {
        this.searchConditions[props] = e.target.value;
        this.forceUpdate();
    }

    // checkchange 函数
    public searchCheckChange = (props: string, e: any) => {
        this.searchConditions[props] = e.target.checked;
        this.forceUpdate();
    }

    // 时间
    public searchDateChange = (date: any, datestring: any, props: any) => {
        if (date) {
            this.searchConditions[props] = new Date(date);
        } else {
            this.searchConditions[props] = null;
        }
        this.forceUpdate();
    }

    // input函数
    public editInputChange = (props: string, e: any) => {
        this.updateData[props] = e.target.value;
        this.forceUpdate();
    }

    // checkchange 函数
    public editCheckChange = (props: string, e: any) => {
        this.updateData[props] = e.target.checked;
        this.forceUpdate();
    }

    // 时间
    public editDateChange = (date: any, datestring: any, props: any) => {
        if (date) {
            this.updateData[props] = new Date(date);
        } else {
            this.updateData[props] = null;
        }

        this.forceUpdate();
    }

    // 搜索change
    public searchChange = (value: any) => {
        this.selectedValue = value;
    }

    // input
    public searchInputValue = (e: any) => {
        this.searchValue = e.target.value;
    }

    // 默认搜索框弹出
    @action
    public searchDataShow = () => {
        if (this.props.columns.length === 0) {
            message.warning('请先添加表！');
            return false;
        }
        this.searchDataFlag = true;
        this.searchValue = '';
    }

    // 默认搜索框关闭
    @action
    public searchDataHide = () => {
        this.searchDataFlag = false;
    }

    // 默认搜索事件
    public searchDefaultData = () => {
        let searchCode = '';
        this.defaultSearchColums.forEach(item => {
            if (item.name === this.selectedValue) {
                searchCode = item.code;
            }
        });
        this.searchConditions[searchCode] = this.searchValue;
        this.searchFlag = true;
        this.searchData();
    }

    // 搜索
    public searchData = () => {
        // 搜索
        const pageSize = this.pagination.pageSize;
        const pageIndex = this.pagination.current;
        const json = JSON.stringify(toJS(this.searchConditions));

        this.selectedData = [];
        this.selectedRowKeys = [];
        tableDataStore.searchTableData(this.props.tableId, pageSize, pageIndex, json).then(res => {
            if (res.result) {
                this.props.searchTable(JSON.parse(res.result.data), res.result.totalCount);
                this.searchFlag = false;
                this.searchConditions = {};
            }
        }).catch(error => {
            alert(error.data.error.message);
        });

    }

    // 高级搜索弹出
    @action
    public searchModalShow = () => {
        let searchColumn = this.props.columns.filter(item => item.searchable);
        if (searchColumn.length === 0) {
            message.warning('没有可搜索的条件！');
            return false;
        }
        // 高级搜索
        this.searchModalFlag = true;
        this.searchConditions = {};
    }

    // 高级搜索确定
    @action
    public searchOk = () => {
        this.searchModalFlag = false;
        this.searchFlag = true;
        this.searchData();
    }

    // 高级搜索取消
    @action
    public searchCancel = () => {
        this.searchModalFlag = false;
    }

    // 启用或禁用数据
    public startData = () => {
        if (this.selectedRowKeys.length === 0) {
            message.warning('请先选择数据！');
            return;
        }
        let flag = false;
        this.selectedData.forEach(item => {
            if (item.IsActive) {
                flag = true;
            }
        });
        if (flag) {
            message.warning('不能选择已启用的数据！');
        } else {
            this.activeData('启用成功！');
        }

    }

    // 停用数据
    public stopData = () => {
        if (this.selectedRowKeys.length === 0) {
            message.warning('请先选择数据！');
            return;
        } else {
            let flag = false;
            this.selectedData.forEach(item => {
                if (!item.IsActive) {
                    // 停用数据
                    flag = true;
                }
            });
            if (flag) {
                message.warning('不能选择已停用的数据！');
            } else {
                this.activeData('停用成功！');
            }
        }
    }

    // 启用或停用数据
    public activeData = (info: string) => {
        let array = [];
        const key = this.primaryKey;
        this.selectedRowKeys.forEach(item => {
            let json = {};
            json[key] = item;
            array.push(json);
        });
        tableDataStore.activeTableData(this.props.tableId, array).then(res => {
            if (res.result) {
                this.searchData();
                message.info(info);
            }
        }).catch(error => {
            alert(error.data.error.message);
        });
    }

    // 新增数据
    @action
    public addData = () => {
        if (this.props.columns.length === 0) {
            message.warning('请先添加表！');
            return false;
        }
        // 新增
        this.editModalFlag = true;
        this.addFlag = true;
        this.primaryFlag = false;
        this.getEmptyData();

    }

    // 修改数据
    public updateTableData = () => {
        // 修改
        if (this.selectedRowKeys.length === 0) {
            message.warning('请先选择数据！');
            return;
        } else if (this.selectedRowKeys.length > 1) {
            message.warning('不能同时修改多条数据！');
            return;
        }
        this.primaryFlag = true;
        this.updateData = JSON.parse(JSON.stringify(this.selectedData[0]));
        this.editModalFlag = true;
        this.addFlag = false;
    }

    // 修改数据确定
    public editOk = () => {
        if (this.checkEmpty()) {
            if (this.addFlag) {
                let flag = false;
                this.props.data.forEach(item => {
                    if (this.updateData[this.primaryKey] === item[this.primaryKey]) {
                        flag = true;
                    }
                });
                if (flag) {
                    message.warning('主键值不能重复！');
                    return;
                }
                // 新增
                tableDataStore.addTableData(this.props.tableId, toJS(this.updateData)).then(res => {
                    if (res.result) {
                        this.searchData();
                        this.editModalFlag = false;
                        message.info('新增成功！');
                    }
                }).catch(error => {
                    alert(error.data.error.message);
                });
            } else {
                // 修改
                tableDataStore.updateTableData(this.props.tableId, toJS(this.updateData)).then(res => {
                    if (res.result) {
                        this.searchData();
                        this.editModalFlag = false;
                        message.info('修改成功！');
                    }
                }).catch(error => {
                    alert(error.data.error.message);
                });
            }
        }
    }

    // 验证数据是否为空
    public checkEmpty(): boolean {
        for (let i = 0; i < this.props.columns.length; i++) {
            const nullable = this.props.columns[i].nullable;
            const code = this.props.columns[i].code;
            if (!nullable) {
                if (this.updateData[code] === '') {
                    message.warning('带*号的为必填项！');
                    return false;
                }
            }
        }
        return true;
    }

    // 修改数据取消
    public editCancel = () => {
        this.editModalFlag = false;
    }

    // rowClassname
    public rowClassName = (record: any, index: number) => {
        if (record.IsActive) {
            return '';
        } else {
            return 'ant-table-row-deactive';
        }
    }

    public importData = () => {
        if (this.props.columns.length === 0) {
            message.warning('请先添加表！');
            return false;
        }
        this.fileInput.click();
    }
    private preview = (e: any) => {
        try {
            readFile(e.target.files[0], file => {
                const data = getJSON(readExcel(file));
                this.previewData = toJS(this.formatData(data));
                this.paginationModel.total = this.previewData.length;
                this.previewchange();
            });
        } catch (err) {
            message.error('读取文件失败');
        }
    }
    private formatData = (data: any[]) => {
        if (!data || data.length < 1) {
            message.error('未获取到数据，请检查文件');
            return;
        }
        this.errRows = [];
        const colName = data.shift();
        const formatDatas = data.map((item, index) => {
            const rowItem: any = { IsActive: true };
            for (const key in item) {
                if (item.hasOwnProperty(key)) {
                    rowItem[colName[key]] = item[key];
                }
            }
            return rowItem;
        });
        return formatDatas;
    }
    private checkRules = (rowItem: any) => {
        const err = {};
        return err;
    }

    @action
    private previewchange = () => {
        /**
         * this.previewData = [];
         * 重置input
         */
        this.fileInput.parentNode.reset();
        this.previewShow = !this.previewShow;
    }
    @action
    private import = () => {

        tableDataStore.importTableData(1, toJS(this.previewData)).then(res => {
            if (res.result) {
                this.searchData();
            }
        }).catch(error => {
            alert(error.data.error.message);
        });
        this.previewchange();
    }

}
