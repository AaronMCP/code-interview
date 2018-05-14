import * as React from 'react';
import { Table, Modal, Checkbox, Form, Input, message } from 'antd';
import { ITableColumn, ITableColumnData } from './interface';
import { EditableCell } from '../../components/common/edit-cell';
import { observer } from 'mobx-react';
import { observable, action, toJS } from 'mobx';
import { TableColumnStore } from '../../store';
import './style.scss';
const tableColumnStore = new TableColumnStore();
const FormItem = Form.Item;
const { TextArea } = Input;
const formLayout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 16 }
};

// 结构默认空值
const columnEmpty = {
    id: '',
    code: '',
    name: '',
    description: '',
    dataType: '',
    default: '',
    nullable: true,
    searchable: false,
    isDefaultSearch: false,
    isPrimary: false,
    editable: false
};
const dataList = ['bigint', 'int', 'smallint', 'tinyint', 'bit', 'float', 'datetime',
    'text', 'ntext', 'char(32)', 'varchar(32)', 'nchar(32)', 'nvarchar(32)'];
// 正数正则
const reg = /^[0-9]*[1-9][0-9]*$/;

@observer
export class TableColumn extends React.Component<ITableColumn, {}> {
    public title: string;
    public height: number;
    public columns: Array<any>;
    public addColumnFlag: boolean;
    public selectedRowData: Array<any>;
    public curColumnDataSource: Array<ITableColumnData>;
    public selectedColumnData: ITableColumnData;
    @observable public columnDataSource: Array<ITableColumnData>;
    @observable public modalColumnFlag: boolean;
    @observable public selectedRowKeys: string[];
    constructor(props: ITableColumn) {
        super(props);
        this.title = '';
        this.selectedColumnData = null;
        this.columnDataSource = [];
        this.curColumnDataSource = [];
        this.addColumnFlag = false;
        this.modalColumnFlag = false;
        // 表格列
        this.columns = this.getColumns();
    }

    // public shouldComponentUpdate(nextProps: any, nextState: any): boolean {
    //     if (nextProps.flag) {
    //         return false;
    //     }
    //     return true;
    // }

    public componentWillReceiveProps(nextProps: any): void {
        let columnDataSource = nextProps.tableColumn;
        if (columnDataSource.length === 0) {
            columnDataSource.push({
                'id': 'IsActive',
                'name': 'IsActive',
                'code': 'IsActive',
                'description': '数据是否启用',
                'dataType': 'bit',
                'nullable': false,
                'searchable': false,
                'isDefaultSearch': false,
                'isPrimary': false
            });
        }
        if (this.props.tableId !== nextProps.tableId) {
            this.modalColumnFlag = false;
        }
        this.columnDataSource = JSON.parse(JSON.stringify(columnDataSource));
        this.curColumnDataSource = JSON.parse(JSON.stringify(this.columnDataSource));
        this.selectedColumnData = columnDataSource[0];
        this.selectedRowData = [columnDataSource[0]];
        this.selectedRowKeys = [columnDataSource[0].id];
    }

    public render(): JSX.Element {
        const rowSelection = {
            onChange: (selectedRowKeys, selectedRows) => {
                this.selectedRowData = selectedRows;
                this.selectedColumnData = selectedRows[0];
                this.selectedRowKeys = selectedRowKeys;
            },
            selectedRowKeys: this.selectedRowKeys,
            type: ('radio' as 'checkbox' | 'radio')
        };
        return (
            <div className='table-constructor'>
                <Table columns={this.columns}
                    scroll={{ x: 1300, y: 400 }}
                    rowKey={(record: any) => { return record.id; }}
                    onRowClick={this.rowClick}
                    dataSource={toJS(this.columnDataSource)}
                    pagination={false}
                    rowSelection={rowSelection}>
                </Table>
                <div className='operate-column-container'>
                    {
                        this.modalColumnFlag ?
                            <div className='operate-constructor operate-container'>
                                {
                                    this.addColumnFlag ?
                                        <div className='common-operate' onClick={this.deleteColumn}>
                                            <div className='common-operate-background'></div>
                                            <span>删除</span>
                                        </div>
                                        :
                                        ''
                                }
                                {
                                    this.addColumnFlag ?
                                        <div className='common-operate' onClick={this.addColumn}>
                                            <div className='common-operate-background'></div>
                                            <span>添加</span>
                                        </div>
                                        :
                                        ''
                                }
                                <div className='common-operate' onClick={this.columnSure}>
                                    <div className='common-operate-background'></div>
                                    <span>完成</span>
                                </div>
                                <div className='common-operate' onClick={this.columnCancel}>
                                    <div className='common-operate-background'></div>
                                    <span>取消</span>
                                </div>
                            </div>
                            :
                            <div className='operate-constructor operate-container'>
                                <div className='common-operate' onClick={this.updateColumn}>
                                    <div className='common-operate-background'></div>
                                    <span>修改</span>
                                </div>
                                <div className='common-operate' onClick={this.addColumn}>
                                    <div className='common-operate-background'></div>
                                    <span>添加</span>
                                </div>
                                <div className='common-operate' onClick={() => { alert('导入'); }}>
                                    <div className='common-operate-background'></div>
                                    <span>导入</span>
                                </div>
                            </div>
                    }
                </div>
            </div>
        );
    }

    public getColumns = () => {
        return [{
            title: '列名',
            dataIndex: 'code',
            width: '100px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'code', 'string');
            }
        }, {
            title: '名称',
            dataIndex: 'name',
            width: '100px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'name', 'string');
            }
        }, {
            title: '描述',
            dataIndex: 'description',
            width: '100px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'description', 'string');
            }
        }, {
            title: '数据类型',
            dataIndex: 'dataType',
            width: '120px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'dataType', 'string', true, dataList);
            }
        }, {
            title: '允许Null值',
            dataIndex: 'nullable',
            width: '80px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'nullable', 'boolean');
            }
        }, {
            title: '主键',
            dataIndex: 'isPrimary',
            width: '60px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'isPrimary', 'boolean');
            }
        }, {
            title: '默认为搜索条件',
            dataIndex: 'isDefaultSearch',
            width: '120px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'isDefaultSearch', 'boolean');
            }
        }, {
            title: '可搜索',
            dataIndex: 'searchable',
            width: '60px',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'searchable', 'boolean');
            }
        }];
    }

    // 编辑表格render
    public renderColumn
        (text: any, record: any, index: number, column: any, dataType: any, selectAble?: any, selectList?: any)
        : JSX.Element {
        return <EditableCell
            editable={record.editable}
            value={text}
            onChange={value => { this.columnChange(column, index, value); }}
            dataType={dataType}
            selectAble={selectAble}
            selectList={selectList}
        />;
    }

    // change函数
    public columnChange = (column: string, index: number, value: string) => {
        this.columnDataSource[index][column] = value;
    }

    // 添加表结构
    public addColumn = () => {
        this.modalColumnFlag = true;
        this.addColumnFlag = true;
        columnEmpty.editable = true;
        columnEmpty.id = 'column' + this.columnDataSource.length + new Date();
        this.columnDataSource.unshift(columnEmpty);
    }

    // 修改表结构
    @action
    public updateColumn = () => {
        if (this.props.tableId) {
            if (this.selectedRowKeys.length === 0) {
                message.warning('请先选择一行数据！');
            } if (this.selectedColumnData.code === 'IsActive') {
                message.warning('IsActive列不能修改！');
            } else {
                this.modalColumnFlag = true;
                this.addColumnFlag = false;
                let columnData = toJS(this.columnDataSource);
                for (let i = 0; i < columnData.length; i++) {
                    if (this.selectedColumnData.id === columnData[i].id) {
                        columnData[i].editable = true;
                    }
                }
                this.columnDataSource = columnData;
            }
        } else {
            message.warning('请先添加表！');
        }
    }

    // 删除未完成的表结构
    public deleteColumn = () => {
        if (this.selectedRowKeys.length === 0) {
            message.warning('请选择需要删除的数据！');
            return;
        }
        if (this.selectedColumnData.id === 'IsActive') {
            message.warning('IsActive列不能删除！');
            return;
        }
        let keys = this.selectedRowKeys[0];
        if (reg.test(keys)) {
            message.warning('只能删除正在添加的数据！');
            return;
        }
        let ind = 0;
        this.columnDataSource.forEach((item, index) => {
            if (item.id === keys) {
                ind = index;
            }
        });
        let data = toJS(this.columnDataSource);
        data.splice(ind, 1);
        this.columnDataSource = data;
        this.selectedRowKeys = [];
        this.selectedColumnData = null;
        this.selectedRowData = [];
    }

    // 确定
    // tslint:disable-next-line:cyclomatic-complexity
    public columnSure = () => {
        if (this.addColumnFlag) {
            // 新增的数据
            let newColumnData = this.columnDataSource.filter((item, index) => {
                if (reg.test(item.id)) {
                    return false;
                }
                return true;
            });
            // 原始数据
            let oldColumnData = this.columnDataSource.filter((item, index) => {
                if (reg.test(item.id)) {
                    return true;
                }
                return false;
            });
            if (newColumnData.length === 0) {
                message.warning('要添加的数据不能为空！');
                return;
            }
            for (let i = 0; i < newColumnData.length; i++) {
                let col = this.columnDataSource.filter(p => p.code === newColumnData[i].code);
                if (col.length > 1) {
                    message.warning('图表结构列名不能重复！');
                    return;
                }
            }
            // 没有原始数据
            if (oldColumnData.length === 0) {
                // 验证是否有主键和IsActive
                let primaryFlag = false;
                let primaryNullableFlag = false;
                let activeFlag = false;
                let activeBooleanFlag = false;
                newColumnData.forEach(item => {
                    const dataType = item.dataType.toLowerCase();
                    if (item.isPrimary) {
                        primaryFlag = true;
                        if (item.nullable) {
                            primaryNullableFlag = true;
                        }
                    }
                    if (item.code === 'IsActive') {
                        activeFlag = true;
                        if (dataType === 'bit') {
                            activeBooleanFlag = true;
                        }
                    }
                });
                if (!primaryFlag) {
                    message.warning('必须添加一个主键！');
                    return;
                } else if (primaryNullableFlag) {
                    message.warning('主键不允许为Null！');
                    return;
                }
                if (!activeFlag) {
                    message.warning('必须添加列名为“IsActive”的图表结构！');
                    return;
                } else if (!activeBooleanFlag) {
                    message.warning('IsActive的数据类型必须为Bit！');
                    return;
                }
            } else {
                // 有原始数据
                for (let i = 0; i < newColumnData.length; i++) {
                    if (newColumnData[i].isPrimary) {
                        if (newColumnData[i].nullable) {
                            message.warning('主键不允许为Null!');
                            return;
                        }
                    }
                }
            }
            if (this.checkColumn(newColumnData)) {
                let newData = JSON.parse(JSON.stringify(newColumnData));
                for (let i = 0; i < newData.length; i++) {
                    delete newData[i].id;
                }
                tableColumnStore.addColums(this.props.tableId, newData).then(res => {
                    if (res.result) {
                        tableColumnStore.getColums(this.props.tableId).then(res2 => {
                            this.columnDataSource = res2.result;
                            this.curColumnDataSource = JSON.parse(JSON.stringify(this.columnDataSource));
                            this.props.columnSure(toJS(this.columnDataSource));
                            this.selectedRowData = this.columnDataSource.length > 0
                                ? [this.columnDataSource[0]] : [];
                            this.selectedRowKeys = this.columnDataSource.length > 0
                                ? [this.columnDataSource[0].id] : [];
                            this.modalColumnFlag = false;
                            message.info('图表结构新增成功！');
                        }).catch(error => {
                            message.error(error.data.error.message);
                        });
                    }
                }).catch(error => {
                    message.error('新增失败，错误：' + error.data.error.message);
                });
            } else {
                return;
            }

        } else {
            let updateData;
            this.columnDataSource.forEach(item => {
                if (item.editable) {
                    updateData = item;
                }
            });
            let clos = this.columnDataSource.filter(c => c.code === updateData.code);
            if (clos.length > 1) {
                message.warning('图表结构列名不能重复！');
                return;
            }
            let primaryFlag = this.columnDataSource.filter(c => c.isPrimary === true);
            if (primaryFlag.length < 1) {
                message.warning('图表结构必须有一列是主键！');
                return;
            }
            let primaryNullFlag = this.columnDataSource.filter(c => c.isPrimary === true && c.nullable === true);
            if (primaryFlag.length < 1) {
                message.warning('主键不允许为Null！');
                return;
            }
            tableColumnStore.updateColums(this.props.tableId, updateData).then(res => {
                if (res.result.success) {
                    tableColumnStore.getColums(this.props.tableId).then(res2 => {
                        this.columnDataSource = res2.result;
                        this.curColumnDataSource = JSON.parse(JSON.stringify(this.columnDataSource));
                        this.props.columnSure(toJS(this.columnDataSource));
                        this.modalColumnFlag = false;
                        message.info('图表结构修改成功！');
                    });
                } else {
                    alert('修改失败，错误：' + res.result.message);
                }
            });
        }
    }

    // 验证数据合法性
    public checkColumn(columnData: Array<any>): boolean {
        for (let i = 0; i < columnData.length; i++) {
            const dataType = columnData[i].dataType.toLowerCase();
            if (columnData[i].code.replace(/(^\s*)|(\s*$)/g, '') === '') {
                message.warning('列名不能为空！');
                return false;
            }
            if (columnData[i].name.replace(/(^\s*)|(\s*$)/g, '') === '') {
                message.warning('名称不能为空！');
                return false;
            }
            if (columnData[i].dataType.replace(/(^\s*)|(\s*$)/g, '') === '') {
                message.warning('数据类型不能为空！');
                return false;
            } else {
                // tslint:disable-next-line:max-line-length
                const dataTypereg = /^(bigint|int|smallint|tinyint|bit|float|datetime|text|ntext)$|^(char|varchar|nchar|nvarchar)\(([1-9]\d*)\)$/;
                if (!dataTypereg.test(dataType)) {
                    message.warning('数据类型必须满足数据库数据类型的命名规范！');
                    return false;
                }
            }
        }
        return true;
    }

    // 添加表结构取消
    public columnCancel = () => {
        this.modalColumnFlag = false;
        this.columnDataSource = JSON.parse(JSON.stringify(this.curColumnDataSource));
        if (!reg.test(this.selectedRowKeys[0])) {
            this.selectedColumnData = this.columnDataSource.length > 0 ? this.columnDataSource[0] : columnEmpty;
            this.selectedRowKeys = this.columnDataSource.length > 0 ? [this.columnDataSource[0].id] : [];
            this.selectedRowData = this.columnDataSource.length > 0 ? [this.columnDataSource[0]] : [];
        }
    }

    // 选中行
    public rowClick = (record: any, index: number, event: any) => {
        this.selectedRowKeys = [record.id];
        this.selectedColumnData = record;
        this.selectedRowData = [record];
    }
}
