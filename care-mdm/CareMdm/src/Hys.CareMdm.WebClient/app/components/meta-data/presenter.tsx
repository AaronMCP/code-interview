import * as React from 'react';
import { Table, Menu, Icon, Button, Modal, Tabs, Input, message } from 'antd';
import { observer } from 'mobx-react';
import { observable, action, toJS } from 'mobx';
import { MetaDataStore } from '../../store';
import { EditableCell } from '../common/edit-cell';
import { IOutInterface, IParamter } from './interface';
import './style.scss';

const metaDataStore = new MetaDataStore();
const TabPane = Tabs.TabPane;
const SubMenu = Menu.SubMenu;
const emptyParamter = {
    id: '',
    outInterfaceId: 0,
    paramName: '',
    paramValue: '',
    description: '',
    editable: false
};

// 正数正则
const reg = /^[0-9]*[1-9][0-9]*$/;

@observer
export class MetaData extends React.Component<{}, {}> {
    @observable public selectedData: Array<any>;
    @observable public selectedParamter: IParamter;
    @observable public selectedRowKeys: Array<any>;
    @observable public outInterface: IOutInterface;
    @observable public paramters: Array<IParamter>;
    @observable public curParamters: Array<IParamter>;
    @observable public showFlag: boolean;
    @observable public addFlag: boolean;
    constructor(props: {}) {
        super(props);
        this.selectedData = [];
        this.selectedRowKeys = [];
        this.showFlag = false;
        this.addFlag = false;
        this.outInterface = {
            id: 0,
            routeName: '',
            description: ''
        };
        this.paramters = [];
        this.curParamters = [];
    }

    public componentDidMount(): void {
        // 获取接口信息
        metaDataStore.getAllInterfaces().then(res => {
            // 获取参数信息
            if (res.result.length > 0) {
                this.outInterface = res.result[0];
                metaDataStore.getParameterById(this.outInterface.id).then(res2 => {
                    this.paramters = res2.result;
                    this.curParamters = JSON.parse(JSON.stringify(this.paramters));
                    this.selectedRowKeys = this.paramters.length > 0 ? [this.paramters[0].id] : [];
                    this.selectedParamter = this.paramters.length > 0 ? this.paramters[0] : null;
                    this.selectedData = this.paramters.length > 0 ? [this.paramters[0]] : [];
                }).catch(error => {
                    alert(error.data.error.message);
                });
            } else {
                this.outInterface = {
                    id: 0,
                    routeName: '',
                    description: ''
                };
            }
        }).catch(error => {
            alert(error.data.error.message);
        });
    }

    public render(): JSX.Element {
        const rowSelection = {
            onChange: (selectedRowKeys, selectedRows) => {
                this.selectedData = selectedRows;
                this.selectedParamter = this.selectedData[0];
                this.selectedRowKeys = selectedRowKeys;
            },
            selectedRowKeys: this.selectedRowKeys.slice(0),
            type: ('radio' as 'checkbox' | 'radio')
        };

        const paramtersColums = [{
            'title': '接口ID',
            'dataIndex': 'outInterfaceId',
            'width': '10%'
        }, {
            'title': '参数名',
            'dataIndex': 'paramName',
            'width': '30%',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'paramName', 'string');
            }
        }, {
            'title': '参数值',
            'dataIndex': 'paramValue',
            'width': '30%',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'paramValue', 'string');
            }
        }, {
            'title': '描述',
            'dataIndex': 'description',
            'width': '30%',
            render: (text: any, record: any, index: number) => {
                return this.renderColumn(text, record, index, 'description', 'string');
            }
        }];
        return (
            <div className='meta-data'>
                <Tabs>
                    <TabPane tab='接口注册' key='1'>
                        <div className='interface-info'>
                            <div>接口名：
                                <Input value={this.outInterface.routeName} />
                            </div>
                            <div>描述：
                                <Input value={this.outInterface.description} />
                            </div>
                        </div>
                        <div className='interface-operate'>
                            {
                                this.showFlag ?
                                    <div className='paramter-container'>
                                        {
                                            this.addFlag ?
                                                <div className='common-operate' onClick={this.paramterDelete}>
                                                    <div className='common-operate-background'></div>
                                                    <span>删除</span>
                                                </div>
                                                :
                                                ''
                                        }
                                        {
                                            this.addFlag ?
                                                <div className='common-operate' onClick={this.paramterAdd}>
                                                    <div className='common-operate-background'></div>
                                                    <span>添加</span>
                                                </div>
                                                :
                                                ''
                                        }
                                        <div className='common-operate' onClick={this.paramterSure}>
                                            <div className='common-operate-background'></div>
                                            <span>完成</span>
                                        </div>
                                        <div className='common-operate' onClick={this.paramterCancel}>
                                            <div className='common-operate-background'></div>
                                            <span>取消</span>
                                        </div>
                                    </div>
                                    :
                                    <div className='paramter-container'>
                                        <div className='common-operate' onClick={this.paramterUpdate}>
                                            <div className='common-operate-background'></div>修改
                                        </div>
                                        <div className='common-operate' onClick={this.paramterAdd}>
                                            <div className='common-operate-background'></div>添加
                                        </div>
                                    </div>
                            }

                        </div>
                        <Table
                            rowKey={(record: any) => { return record.id; }}
                            pagination={false}
                            columns={paramtersColums}
                            dataSource={this.paramters.slice(0)}
                            rowSelection={rowSelection}
                            onRowClick={this.rowClick}
                        >
                        </Table>
                    </TabPane>
                    <TabPane tab='厂商注册' key='2'>
                        厂商注册
                    </TabPane>
                </Tabs>
            </div>
        );
    }

    // 编辑表格render
    public renderColumn(text: any, record: any, index: number, column: any, dataType: any): JSX.Element {
        return <EditableCell
            editable={record.editable}
            value={text}
            onChange={value => { this.paramtersChange(column, index, value); }}
            dataType={dataType}
        />;
    }

    // change函数
    public paramtersChange = (column: string, index: number, value: string) => {
        this.paramters[index][column] = value;
    }

    // 添加
    public paramterAdd = () => {
        this.addFlag = true;
        this.showFlag = true;
        emptyParamter.editable = true;
        emptyParamter.outInterfaceId = this.outInterface.id;
        emptyParamter.id = 'paramter' + this.paramters.length + new Date();
        this.paramters.unshift(emptyParamter);
    }

    // 修改
    public paramterUpdate = () => {
        if (this.selectedRowKeys.length === 0) {
            message.warning('请先选择一行数据！');
        } else {
            this.addFlag = false;
            this.showFlag = true;
            let paramterData = toJS(this.paramters);
            for (let i = 0; i < paramterData.length; i++) {
                if (this.selectedParamter.id === paramterData[i].id) {
                    paramterData[i].editable = true;
                }
            }
            this.paramters = paramterData;
        }
    }

    // 完成
    public paramterSure = () => {
        if (this.addFlag) {
            // 新增的数据
            let newParamterData = this.paramters.filter((item, index) => {
                if (reg.test(item.id)) {
                    return false;
                }
                return true;
            });
            // 原始数据
            let oldParamterData = this.paramters.filter((item, index) => {
                if (reg.test(item.id)) {
                    return true;
                }
                return false;
            });
            if (newParamterData.length === 0) {
                message.warning('要添加的数据不能为空！');
                return;
            }
            if (this.checkParamter()) {
                for (let i = 0; i < newParamterData.length; i++) {
                    delete newParamterData[i].id;
                    delete newParamterData[i].editable;
                }
                metaDataStore.addOutParameter(newParamterData[0]).then(res => {
                    if (res.result) {
                        metaDataStore.getParameterById(this.outInterface.id).then(res2 => {
                            if (res2.result) {
                                this.paramters = res2.result;
                                this.curParamters = JSON.parse(JSON.stringify(this.paramters));
                                message.info('参数新增成功！');
                                this.showFlag = false;
                            }
                        }).catch(error => {
                            alert(error.data.error.message);
                        });
                    } else {
                        message.warning('参数名不能重复！');
                        return;
                    }
                }).catch(error => {
                    alert(error.data.error.message);
                });
            }
        } else {
            let updateData;
            this.paramters.forEach(item => {
                if (item.editable) {
                    updateData = item;
                }
            });
            metaDataStore.updateOutParameter(updateData).then(res => {
                if (res.result) {
                    metaDataStore.getParameterById(this.outInterface.id).then(res2 => {
                        if (res2.result) {
                            this.paramters = res2.result;
                            this.curParamters = JSON.parse(JSON.stringify(this.paramters));
                            message.info('参数修改成功！');
                            this.showFlag = false;
                        }
                    }).catch(error => {
                        alert(error.data.error.message);
                    });
                } else {
                    message.warning('参数名不能重复！');
                    return;
                }
            }).catch(error => {
                alert(error.data.error.message);
            });
        }
    }

    // 验证数据合法性
    public checkParamter(): boolean {
        for (let i = 0; i < this.paramters.length; i++) {
            if (this.paramters[i].paramName.replace(/(^\s*)|(\s*$)/g, '') === '') {
                message.warning('参数名不能为空！');
                return false;
            }
            if (this.paramters[i].paramValue.replace(/(^\s*)|(\s*$)/g, '') === '') {
                message.warning('参数名不能为空！');
                return false;
            }
        }
        return true;
    }

    // 取消
    public paramterCancel = () => {
        this.showFlag = false;
        this.paramters = JSON.parse(JSON.stringify(this.curParamters));
        if (!reg.test(this.selectedRowKeys[0])) {
            this.selectedParamter = this.paramters.length > 0 ? this.paramters[0] : null;
            this.selectedRowKeys = this.paramters.length > 0 ? [this.paramters[0].id] : [];
            this.selectedData = this.paramters.length > 0 ? [this.paramters[0]] : [];
        }
    }

    // 删除
    public paramterDelete = () => {
        if (this.selectedRowKeys.length === 0) {
            message.warning('请选择需要删除的数据！');
            return;
        }
        let keys = this.selectedRowKeys[0];
        if (reg.test(keys)) {
            message.warning('只能删除正在添加的数据！');
            return;
        }
        let ind = 0;
        this.paramters.forEach((item, index) => {
            if (item.id === keys) {
                ind = index;
            }
        });
        let data = toJS(this.paramters);
        data.splice(ind, 1);
        this.paramters = data;
        this.selectedRowKeys = [];
        this.selectedParamter = null;
        this.selectedData = [];
    }

    // 选中行
    public rowClick = (record: any, index: number, event: any) => {
        this.selectedRowKeys = [record.id];
        this.selectedData = [record];
        this.selectedParamter = record;
    }
}
