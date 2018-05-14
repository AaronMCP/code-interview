import * as React from 'react';
import { ITableGroupItem } from './interface';
import { Button, Modal, Popconfirm, Menu, Icon, Input, Tabs, Row, Col, Form, message } from 'antd';
import { TableColumn, ITableColumnData } from '../table-column';
import { TableDescription } from '../table-desciption';
import { TableData } from '../table-data';
import { ITableList, TableList, ITableListData } from '../table-list';
import { TableGroupStore, TableDescriptionStore, TableColumnStore, TableDataStore } from '../../store';
import { observer } from 'mobx-react';
import { observable, action, toJS, runInAction } from 'mobx';
import './style.scss';

const FormItem = Form.Item;
const formLayout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 16 }
};

const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;
const TabPane = Tabs.TabPane;

const tableGroupStore = new TableGroupStore();
const tableDescriptionStore = new TableDescriptionStore();
const tableColumnStore = new TableColumnStore();
const tableDataStore = new TableDataStore();

const tableDescription = {
    id: 0,
    groupId: 0,
    standardCode: '',
    standardCnName: '',
    name: '',
    standardEnName: '',
    code: '',
    isActive: true,
    activeTime: new Date(),
    deactiveTime: null,
    description: '',
    comments: ''
};

@observer
export class MainData extends React.Component<{}, {}> {
    // 表分组
    public firstGroupParentId: number;
    public secondGroupParentId: number;
    public tableParentId: number;
    public firstFlag: boolean;
    public addFlag: boolean;
    public defaultFirstOpenKeys: string[];
    public firstSelectedKeys: string[];
    public secondSelectedKeys: string[];
    public updateGroupData: ITableGroupItem;
    @observable public newGroup: any;
    @observable public modalFlag: boolean;
    @observable public groupData: Array<any>;
    @observable public tableGroupData: Array<ITableGroupItem>;
    @observable public tables: Array<ITableListData>;

    // 图表结构
    public tableId: number;
    @observable public tableFlag: boolean;
    @observable public tableColumn: Array<ITableColumnData>;

    // 图表数据
    public total: number;
    @observable public tableData: any;
    @observable public tableDataColumn: Array<ITableColumnData>;

    // 数据描述
    public addListFlag: boolean;
    public groupText: string;
    @observable public modalListFlag: boolean;
    @observable public tableListData: ITableListData;
    @observable public tableDescription: ITableListData;

    constructor(props: {}) {
        super(props);
        this.newGroup = {
            parentId: 0,
            name: '',
            isActive: 1
        };
        this.groupData = [];
        this.secondGroupParentId = 0;
        this.modalFlag = false;
        this.addFlag = false;
        this.firstGroupParentId = 0;
        this.tableParentId = 0;
        this.tableGroupData = [];
        this.tables = [];
        this.firstFlag = false;
        this.defaultFirstOpenKeys = [];
        this.firstSelectedKeys = [];
        this.secondSelectedKeys = [];

        // 图表结构
        this.tableColumn = [];
        this.tableId = 0;
        this.tableFlag = false;

        // 图表数据
        this.tableData = [];
        this.tableDataColumn = [];
        this.total = 0;

        // 数据描述
        this.addListFlag = false;
        this.modalListFlag = false;
        this.groupText = '';
        this.tableListData = tableDescription;
        this.tableDescription = tableDescription;
    }

    @action
    public componentDidMount(): void {
        // 获取分组
        tableGroupStore.getAllTableGroups().then(res => {
            runInAction(() => {
                this.tableGroupData = res.result;
                // 默认请求第一组的表
                if (this.tableGroupData.length > 0) {
                    this.groupData = this.getGroupData();
                    if (this.groupData.length > 0) {
                        if (this.groupData[0].children.length > 0) {
                            this.getAllTables(this.groupData[0].children[0].id);
                        }
                    }
                }
            });
        }).catch(error => {
            alert(error.data.error.message);
        });
    }

    public render(): JSX.Element {
        return (
            <div className='main-data'>
                {
                    this.getGroupList()
                }
                {
                    this.getGroupModal()
                }
                {
                    this.tableParentId ?
                        <div className='main-data-right'>
                            <div className='table-menu-container'>
                                <div className='third-operate-container'>
                                    <div className='third-operate' onClick={() => { this.addTableList(); }}>
                                        <Icon type='plus-circle-o' /></div>
                                    <div className='search-operate' onClick={() => { alert('search'); }}>
                                        <span className='icon-search'></span></div>
                                </div>
                                {
                                    this.getTableList()
                                }
                                <TableList
                                    groupText={this.groupText}
                                    tableListData={this.tableListData}
                                    onCancel={this.tableListCancel}
                                    onOk={this.tableListOk}
                                    modalListFlag={this.modalListFlag}
                                    addListFlag={this.addListFlag}
                                    inputChange={this.inputChange}
                                    dateChange={this.dateChange}
                                    checkChange={this.checkChange} />
                            </div>
                            {
                                this.tableFlag ?
                                    <div className='mdm-content'>
                                        <Tabs type='card' >
                                            <TabPane tab='图表结构' key='1'>
                                                <TableColumn
                                                    tableId={this.tableId}
                                                    tableColumn={this.tableColumn}
                                                    columnSure={this.columSure}
                                                />
                                            </TabPane>
                                            <TabPane tab='图表数据' key='2'>
                                                <TableData
                                                    data={this.tableData}
                                                    columns={this.tableDataColumn}
                                                    tableId={this.tableId}
                                                    total={this.total}
                                                    searchTable={this.searchTable} />
                                            </TabPane>
                                            <TabPane tab='数据描述' key='3'>
                                                <TableDescription
                                                    update={this.updateTableList}
                                                    tableDescription={this.tableDescription} />
                                            </TabPane>
                                        </Tabs>
                                    </div>
                                    :
                                    ''
                            }
                        </div>
                        :
                        ''
                }
            </div>
        );
    }

    //#region 页面初始化
    // 获得左侧menu栏
    // 分组数据重组
    public getGroupData(): Array<any> {
        let menuData = [];
        this.tableGroupData.forEach((item) => {
            if (item.name === '主数据管理') {
                this.firstGroupParentId = item.id;
            }
        });
        // 二级分组
        this.tableGroupData.forEach((item, index) => {
            if (item.parentId === this.firstGroupParentId) {
                menuData.push({
                    'id': item.id,
                    'parentId': item.parentId,
                    'name': item.name,
                    'children': []
                });
            }
        });
        if (menuData.length > 0) {
            if (this.defaultFirstOpenKeys.length === 0) {
                this.defaultFirstOpenKeys = [menuData[0].id + ''];
            }
            // 三级分组
            this.tableGroupData.forEach((item) => {
                for (let i = 0; i < menuData.length; i++) {
                    if (menuData[i].id === item.parentId) {
                        menuData[i].children.push(item);
                    }
                }
            });
            if (menuData[0].children.length > 0) {
                if (this.firstSelectedKeys.length === 0) {
                    this.firstSelectedKeys = [menuData[0].children[0].id + ''];
                    this.tableParentId = menuData[0].children[0].id;
                }
                if (this.groupText === '') {
                    this.groupText = menuData[0].children[0].name;
                }
            }
        }
        return menuData;
    }

    // 根据id获取表
    @action
    public getAllTables = (id: number, callBack?: Function) => {
        tableDescriptionStore.getTableByGroup(id).then(res => {
            runInAction(() => {
                this.tables = res.result;
                // 请求表结构
                if (this.tables.length > 0) {
                    let selectTable = this.tables.filter(item => item.id === this.tableId);
                    // 未点击左侧menu
                    if (selectTable.length > 0) {
                        this.secondSelectedKeys = [this.tableId + ''];
                        this.tableDescription = JSON.parse(JSON.stringify(selectTable[0]));
                        this.tableId = this.tableDescription.id;
                        if (callBack) {
                            callBack();
                        }
                    } else {
                        this.tableId = this.tables[0].id;
                        this.secondSelectedKeys = [this.tableId + ''];
                        this.tableDescription = JSON.parse(JSON.stringify(this.tables[0]));
                        this.tableFlag = true;
                        tableColumnStore.getColums(this.tableId).then(res => {
                            runInAction(() => {
                                this.tableColumn = res.result;
                                this.tableDataColumn = JSON.parse(JSON.stringify(res.result));
                                if (this.tableColumn.length > 1) {
                                    // 获图表数据
                                    const json = JSON.stringify({});
                                    tableDataStore.searchTableData(this.tableId, 20, 1, json).then(res => {
                                        this.total = res.result.totalCount;
                                        this.tableData = JSON.parse(res.result.data);
                                        if (callBack) {
                                            callBack();
                                        }
                                    }).catch(error => {
                                        alert(error.data.error.message);
                                    });
                                } else {
                                    this.tableData = [];
                                    this.total = 0;
                                }
                            });
                        }).catch(error => {
                            alert(error.data.error.message);
                        });
                    }
                } else {
                    this.tableId = 0;
                    this.tableFlag = false;
                    this.tableDescription = tableDescription;
                    this.tableColumn = [];
                    this.tableDataColumn = [];
                }
            });
        }).catch(error => {
            alert(error.data.error.message);
        });
    }
    // 渲染分组
    public getGroupList(): JSX.Element {
        if (this.tableGroupData.length > 0) {
            return (
                <div className='menu-list'>
                    <Menu className='main-data-menu-first'
                        mode='inline'
                        onClick={this.menuClick}
                        defaultOpenKeys={toJS(this.defaultFirstOpenKeys)}
                        selectedKeys={toJS(this.firstSelectedKeys)}
                    >
                        {
                            this.groupData.map((item) => {
                                return (
                                    <SubMenu key={item.id} title={
                                        <span>
                                            <span>{item.name}</span>
                                            <span className='group-edit group-edit-first'
                                                onClick={(e) => this.editGroup(e, item.id)}></span>
                                        </span>
                                    }>
                                        <div className='first-menu-add'>
                                            <div className='second-add-background' onClick={this.secondAdd}></div>
                                        </div>
                                        {
                                            item.children.length > 0 ? item.children.map((ite) => {
                                                return (
                                                    <Menu.Item key={ite.id}>
                                                        <div className='group-edit group-edit-second'
                                                            onClick={(e) => this.editGroup(e, ite.id)}></div>
                                                        {ite.name}
                                                    </Menu.Item>);
                                            }) : ''
                                        }
                                    </SubMenu>);
                            })
                        }
                        <div className='first-menu-add'>
                            <div className='first-add-backgroud' onClick={this.firstAdd}></div>
                        </div>
                    </Menu>
                </div>
            );
        } else {
            return (
                <div className='menu-list'>
                    <div className='first-menu-add'>
                        <div className='first-add-backgroud' onClick={this.firstAdd}></div>
                    </div>
                </div>);
        }
    }

    // 渲染表List
    public getTableList = () => {
        if (this.tables.length > 0) {
            return (
                <Menu className='main-data-menu-second'
                    onClick={this.tableListClick}
                    selectedKeys={this.secondSelectedKeys}
                >
                    {
                        this.tables.map((item) => {
                            return (
                                <Menu.Item key={item.id}>
                                    {
                                        item.isActive ? null : <div className='menu-is-active'></div>
                                    }
                                    {item.name}
                                </Menu.Item>
                            );
                        })
                    }
                </Menu>
            );
        }
    }
    //#endregion

    //#region 表分组

    // 点击左侧menu栏
    @action
    public menuClick = (e: any) => {
        if (e.key) {
            this.secondGroupParentId = Number(e.keyPath[1]);
            this.tableParentId = e.key;
            this.firstSelectedKeys = [this.tableParentId + ''];
            if (this.tableParentId) {
                this.groupText = e.item.props.children[1];
                // 获取所有表
                this.getAllTables(this.tableParentId);
            }
        } else if (e.keyPath) {
            this.secondGroupParentId = Number(e.keyPath[0]);
        }
    }

    // 添加表分组
    public getGroupModal = () => {
        return (
            <Modal
                visible={this.modalFlag}
                onCancel={() => { this.modalFlag = false; }}
                onOk={this.groupAddSure}
                title={this.addFlag ? '添加分组' : '修改分组'}
                cancelText='取消'
                okText='确定'
            >
                <FormItem {...formLayout} label='* 组名' className='table-form-required'>
                    <Input value={this.newGroup.name}
                        onPressEnter={(e) => this.groupAddSure()}
                        onChange={(e) => this.groupInputChange('name', e)} />
                </FormItem>
            </Modal>);
    }

    @action
    public groupInputChange = (props: string, e: any) => {
        this.newGroup[props] = e.target.value;
    }

    // 修改分组
    @action
    public editGroup = (e: any, id: number) => {
        // 阻止事件冒泡
        e.stopPropagation();
        e.nativeEvent.stopImmediatePropagation();
        this.modalFlag = true;
        this.addFlag = false;
        this.tableGroupData.forEach(item => {
            if (item.id === id) {
                this.updateGroupData = JSON.parse(JSON.stringify(item));
                this.newGroup.name = item.name;
            }
        });
    }

    // 新增一级分组
    @action
    public firstAdd = () => {
        this.firstFlag = true;
        this.modalFlag = true;
        this.addFlag = true;
        this.newGroup.name = '';
    }

    // 新增二级分组
    @action
    public secondAdd = () => {
        this.firstFlag = false;
        this.modalFlag = true;
        this.addFlag = true;
        this.newGroup.name = '';
    }

    // 分组确定
    @action
    // tslint:disable-next-line:cyclomatic-complexity
    public groupAddSure = () => {
        if (this.newGroup.name.replace(/(^\s*)|(\s*$)/g, '') === '') {
            message.warning('组名不能为空！');
            return;
        }
        // 新增分组
        if (this.addFlag) {
            // 判断重复
            for (let i = 0; i < this.tableGroupData.length; i++) {
                if (this.tableGroupData[i].name === this.newGroup.name) {
                    message.warning('组名不能重复！');
                    return;
                }
            }
            if (this.firstFlag) {
                this.newGroup.parentId = this.firstGroupParentId;
            } else {
                this.newGroup.parentId = this.secondGroupParentId;
            }
            tableGroupStore.addTableGroup(this.newGroup).then(res => {
                if (res.result) {
                    this.modalFlag = false;
                    tableGroupStore.getAllTableGroups().then(res2 => {
                        if (res2.result) {
                            runInAction(() => {
                                message.info('分组新增成功！');
                                this.tableGroupData = res2.result;
                                this.groupData = this.getGroupData();
                            });
                        } else {
                            this.tableGroupData = [];
                        }
                    }).catch(error => {
                        alert(error.data.error.message);
                    });
                }
            }).catch(error => {
                alert(error.data.error.message);
            });
        } else {
            // 修改分组
            this.updateGroupData.name = this.newGroup.name;
            // 判断重复
            for (let i = 0; i < this.tableGroupData.length; i++) {
                if (this.tableGroupData[i].id !== this.updateGroupData.id) {
                    if (this.tableGroupData[i].name === this.updateGroupData.name) {
                        message.warning('组名不能重复！');
                        return;
                    }
                }
            }
            tableGroupStore.updateTableGroup(this.updateGroupData).then(res => {
                if (res.result) {
                    this.modalFlag = false;
                    tableGroupStore.getAllTableGroups().then(res2 => {
                        if (res2.result) {
                            runInAction(() => {
                                message.info('分组修改成功！');
                                this.tableGroupData = res2.result;
                                this.groupData = this.getGroupData();
                            });
                        } else {
                            this.tableGroupData = [];
                        }
                    }).catch(error => {
                        alert(error.data.error.message);
                    });
                }
            }).catch(error => {
                alert(error.data.error.message);
            });
        }

    }

    //#endregion

    //#region 表
    // 表menu点击
    @action
    public tableListClick = (e: any) => {
        this.tableId = Number(e.key);
        this.secondSelectedKeys = [this.tableId + ''];
        // 获取图表结构
        tableColumnStore.getColums(this.tableId).then(res => {
            runInAction(() => {
                this.tableColumn = res.result;
                this.tableDataColumn = JSON.parse(JSON.stringify(res.result));
                if (this.tableColumn.length > 1) {
                    // 获取图表数据
                    let json = JSON.stringify({});
                    tableDataStore.searchTableData(this.tableId, 20, 1, json).then(res => {
                        this.total = res.result.totalCount;
                        this.tableData = JSON.parse(res.result.data);
                    }).catch(error => {
                        alert(error.data.error.message);
                    });
                } else {
                    this.tableData = [];
                    this.total = 0;
                }
            });
            // 获取数据描述
            tableDescriptionStore.getTableById(this.tableId).then(res => {
                this.tableDescription = res.result;
            }).catch(error => {
                alert(error.data.error.message);
            });
        }).catch(error => {
            alert(error.data.error.message);
        });
    }

    @action
    // 添加表menu
    public addTableList = () => {
        this.addListFlag = true;
        this.modalListFlag = true;
        this.tableListData = tableDescription;
        this.tableListData.groupId = this.tableParentId;
    }

    @action
    // 修改表menu
    public updateTableList = () => {
        this.addListFlag = false;
        this.modalListFlag = true;
        this.tableListData = JSON.parse(JSON.stringify(this.tableDescription));
        this.tableListData.groupId = this.tableParentId;
    }

    // 表menu确定
    @action
    public tableListOk = () => {
        // code name不能为空
        if (this.tableListData.code.replace(/(^\s*)|(\s*$)/g, '') === '') {
            message.info('英文简称不能为空！');
            return;
        }
        if (this.tableListData.name.replace(/(^\s*)|(\s*$)/g, '') === '') {
            message.info('中文简称不能为空！');
            return;
        }
        // 启用停用时间
        if (this.tableListData.isActive) {
            this.tableListData.activeTime = new Date();
        } else {
            this.tableListData.deactiveTime = new Date();
        }
        // 新增
        if (this.addListFlag) {
            // 调用接口新增tableList
            tableDescriptionStore.addTableInfo(this.tableListData).then(res => {
                if (res.result > 0) {
                    const text = this.groupText;
                    this.modalListFlag = false;
                    this.getAllTables(this.tableParentId, function (): void {
                        message.info(text + '新增成功！');
                    });
                } else if (res.result === -1) {
                    message.warning('英文简称不能重复！');
                } else {
                    message.warning('新增失败！');
                }
            }).catch(error => {
                alert(error.data.error.message);
            });
        } else {
            // 调用接口修改tableList
            tableDescriptionStore.upateTableInfo(this.tableListData).then(res => {
                if (res.result) {
                    const text = this.tableListData.name;
                    this.modalListFlag = false;
                    this.getAllTables(this.tableParentId, function (): void {
                        message.info(text + '修改成功！');
                    });
                } else if (res.result === -1) {
                    message.warning('英文简称不能重复！');
                } else {
                    message.warning('修改失败！');
                }
            }).catch(error => {
                alert(error.data.error.message);
            });
        }
    }
    // 表menu取消
    public tableListCancel = () => {
        this.modalListFlag = false;
    }

    @action
    public inputChange = (props: string, e: any) => {
        this.tableListData[props] = e.target.value;
        this.forceUpdate();
    }

    @action
    public dateChange = (date: any, datestring: any, props: any) => {
        if (date === null) {
            this.tableListData[props] = null;
        } else {
            this.tableListData[props] = new Date(date);
        }
        this.forceUpdate();
    }

    @action
    public checkChange = (e: any) => {
        this.tableListData.isActive = e.target.checked;
        this.forceUpdate();
    }

    //#endregion

    //#region 图表结构
    // 添加表结构确定
    @action
    public columSure = (data: any) => {
        this.tableColumn = data;
        let json = JSON.stringify({});
        tableDataStore.searchTableData(this.tableId, 20, 1, json).then(res => {
            runInAction(() => {
                this.tableDataColumn = JSON.parse(JSON.stringify(data));
                this.tableData = JSON.parse(res.result.data);
                this.total = res.result.totalCount;
            });
        }).catch(error => {
            alert(error.data.error.message);
        });
    }

    //#endregion

    //#region 表数据
    @action
    public searchTable = (data: any, total: number) => {
        this.tableData = data;
        this.total = total;
    }
    //#endregion

}
