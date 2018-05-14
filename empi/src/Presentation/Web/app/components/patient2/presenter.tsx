import * as React from 'react';
import { IPatient } from './interface';
import {
  Row, Col, Pagination, DatePicker, Button, Table, Modal, Select, Form, Input, message,
  InputNumber
} from 'antd';
import { PatientStore, RuleStore, SourceStore } from '../../store';
import { PatientInfo } from './patient-info';
import moment from 'moment';
import { observer } from 'mobx-react';
import { observable, action, toJS, runInAction } from 'mobx';
import './style.scss';

const patientStore = new PatientStore();
const ruleStore = new RuleStore();
const sourceStore = new SourceStore();
const Option = Select.Option;
const FormItem = Form.Item;
const formLayout = {
  labelCol: { span: 6 },
  wrapperCol: { span: 18 }
};
@observer
export class Patient extends React.Component<IPatient, {}> {
  public primaryKey: string;
  @observable public searchModalFlag: boolean;
  @observable public mergeRecords: boolean;
  // 列表病人信息
  @observable public patientList: Array<any>;
  // 合并后的病人信息
  @observable public patientMergeList: Array<any>;
  // 右侧展示的病人信息
  @observable public patientInfo: any;
  // 合并记录
  @observable public patientHistory: Array<any>;
  // 源数据
  @observable public rawPatient: Array<any>;
  // 选择合并的数据
  @observable public selectedData: Array<any>;
  @observable public selectedRowKeys: Array<any>;

  // 选择拆分的数据
  @observable public selectedDataMerge: Array<any>;
  @observable public selectedRowKeysMerge: Array<any>;
  // 高级搜索条件
  @observable public searchConditions: IPatient;

  // 搜索条件
  @observable public searchValue: string;

  // 可搜索的条件
  @observable public searchList: Array<any>;

  // 选中的搜索条件
  @observable public selectCondition: string;

  //
  @observable public patientMerge: Array<any>;
  @observable public updateData: any;

  public state: any;
  public modalParam: any;
  public columns: any;
  public mergeColumns: any;
  public page: any;
  constructor(props: IPatient) {
    super(props);
    this.searchModalFlag = false;
    this.state = {
      mergeArray: [0],
      dataSourceArray: [0],
      checkdataSourceArray: [0],
      firstTdNum: 0
    };

    this.modalParam = {},
      this.updateData = {};
    this.mergeRecords = false;
    this.patientMergeList = [];

    this.patientHistory = [];
    this.rawPatient = [];
    this.patientInfo = {
      bornProvince: '',
      bornContry: '',
      socialSecurityNo: '',
      credentialType: '',
      credentialNumber: '',
      maritalStatus: '',
      nationality: '',
      phone: '',
      address: ''
    };
    this.searchList = [];

    this.columns = [
      {
        'title': 'EMPIID',
        'dataIndex': 'empiId',
        'width': 120
      }, {
        'title': 'Empi号码',
        'dataIndex': 'empiNumber',
        'width': 120
      }, {
        'title': '患者姓名',
        'dataIndex': 'name',
        'width': 120
      }, {
        'title': '性别',
        'dataIndex': 'sex',
        'width': 80
      }, {
        'title': '出生日期',
        'dataIndex': 'birthday',
        'width': 120,
        render: (text: any, record: any, index: number): JSX.Element => {
          let str = moment().format().substr(0, 10).replace(/-/g, '/');
          return <span>{str}</span>;
        }
      }, {
        'title': '证件号码',
        'dataIndex': 'credentialNumber'
        // 'width': 160
      }, {
        'title': '医保号',
        'dataIndex': 'socialSecurityNo'
        // 'width': 100
      }, {
        'title': '操作',
        'dataIndex': 'operate',
        'width': 150,
        'fixed': 'right',
        render: (text: any, record: any, index: number): JSX.Element => {
          return <div className='patient-table-operate'>
            <span className='patient-records'
              onClick={() => this.viewMergeRecords(record)}>查看合并纪录</span>
          </div>;
        }
      }
    ];
    this.mergeColumns = [
      {
        'title': 'EMPIID',
        'dataIndex': 'empiId',
        'width': 120
      }, {
        'title': 'Empi号码',
        'dataIndex': 'empiNumber',
        'width': 120
      }, {
        'title': '患者姓名',
        'dataIndex': 'name',
        'width': 120
      }, {
        'title': '性别',
        'dataIndex': 'sex',
        'width': 80
      }, {
        'title': '出生日期',
        'dataIndex': 'birthday',
        'width': 120,
        render: (text: any, record: any, index: number): JSX.Element => {
          let str = moment().format().substr(0, 10).replace(/-/g, '/');
          return <span>{str}</span>;
        }
      }, {
        'title': '证件号码',
        'dataIndex': 'credentialNumber',
        'width': 160
      }, {
        'title': '医保号',
        'dataIndex': 'socialSecurityNo',
        'width': 120
      }, {
        'title': '合并日期',
        'dataIndex': 'updateAt',
        'width': 150,
        render: (text: any, record: any, index: number): JSX.Element => {
          let str = moment().format().substr(0, 10).replace(/-/g, '/');
          return <span>{str}</span>;
        }
      }, {
        'title': '操作',
        'dataIndex': 'operate',
        'width': 150,
        render: (text: any, record: any, index: number): JSX.Element => {
          return <div className='patient-table-operate'>
            {record.updateAt ?
              (
                record.children ?
                  <span className='patient-data' onClick={() => this.viewRecords(record.children[0].key)}>查看源数据</span> :
                  // <span className='patient-data'>原始数据</span>
                  ''
              )
              :
              ''
            }

          </div>;
        }
      }
    ];
    this.page = {
      total: 0,
      current: 1,
      pageSize: 10
    };

    this.selectedData = [];
    this.selectedRowKeys = [];
    this.selectedDataMerge = [];
    this.selectedRowKeysMerge = [];
    this.searchConditions = {
      empiId: '',
      empiNumber: '',
      name: '',
      sex: '全部',
      birthday: null,
      credentialType: '',
      credentialNumber: '',
      bornProvince: '',
      bornContry: '',
      medicalInsuranceNo: '',
      socialSecurityNo: '',
      maritalStatus: '',
      nationality: '',
      phone: '',
      address: ''
    };
    this.selectedDataMerge = [];
    this.selectedRowKeysMerge = [];
    this.patientMerge = [
      {
        'id': 10001,
        'key': 10001,
        'updateAt': '2018/05/04 09:10',
        'empiId': 9528,
        'empiNumber': 9528,
        'name': Math.random().toString(36).substr(2),
        'sex': '男',
        'birthday': '1999/11/11',
        'credentialNumber': Math.floor(Math.random() * 999999999999999999),
        'socialSecurityNo': Math.floor(Math.random() * 99999999999),
        'children': [{
          'id': 6666,
          'key': 6666,
          'rule': {
            'rule': '相同',
            'ruleContent': '姓名＋医保号'
          },
          'datasource': 'HIS'
        }]
      },
      {
        'id': 10002,
        'key': 10002,
        'updateAt': '2018/05/04 09:10',
        'empiId': 9529,
        'empiNumber': 9529,
        'name': Math.random().toString(36).substr(2),
        'sex': '男',
        'birthday': '1999/11/11',
        'credentialNumber': Math.floor(Math.random() * 999999999999999999),
        'socialSecurityNo': Math.floor(Math.random() * 99999999999),
        'children': [{
          'id': 7777,
          'key': 7777,
          'rule': {
            'rule': '相似',
            'ruleContent': '姓名＋医保号'
          },
          'datasource': 'HIS'
        }]
      },
      {
        'id': 10003,
        'key': 10003,
        'updateAt': '2018/05/04 09:10',
        'empiId': 9529,
        'empiNumber': 9529,
        'name': Math.random().toString(36).substr(2),
        'sex': '男',
        'birthday': '1999/11/11',
        'credentialNumber': Math.floor(Math.random() * 999999999999999999),
        'socialSecurityNo': Math.floor(Math.random() * 99999999999)
      }
    ];
    this.patientHistory = [
      {
        'id': 111,
        'key': 111,
        'updateAt': '2018/05/04 09:10',
        'empiId': 1100101,
        'empiNumber': 1100101,
        'name': Math.random().toString(36).substr(2),
        'sex': '男',
        'birthday': '1999/11/11',
        'credentialNumber': Math.floor(Math.random() * 999999999999999999),
        'socialSecurityNo': Math.floor(Math.random() * 99999999999),
        'children': [{
          'id': 1116,
          'key': 1116,
          'rule': {
            'rule': '相同',
            'ruleContent': '姓名＋医保号'
          },
          'datasource': 'HIS'
        }]
      },
      {
        'id': 222,
        'key': 222,
        'updateAt': '2018/05/04 09:10',
        'empiId': 1100102,
        'empiNumber': 1100102,
        'name': Math.random().toString(36).substr(2),
        'sex': '男',
        'birthday': '1999/11/11',
        'credentialNumber': Math.floor(Math.random() * 999999999999999999),
        'socialSecurityNo': Math.floor(Math.random() * 99999999999)
      }
    ];
  }

  // 查询病人信息
  public getPatients = (params: any) => {
    patientStore.getPatients(params).then(res => {
      // const data = [];
      // for ( let i = 0; i < 100; i++ ) {
      //   data.push({
      //     'id' : i ,
      //     'key' : i ,
      //     'updateAt' : '' ,
      //     'empiId' : 1100000 + i ,
      //     'empiNumber' : 1100000 + i ,
      //     'name' : Math.random().toString(36).substr(2) ,
      //     'sex' : '男' ,
      //     'birthday' : '1965/05/26' ,
      //     'credentialNumber' : Math.floor(Math.random() * 999999999999999999) ,
      //     'socialSecurityNo' : Math.floor(Math.random() * 99999999999)
      //   });
      // }
      // this.patientList = data;
      this.page.total = res.total;
      this.patientList = res.patients;
      this.patientInfo = res.total > 0 ? res.patients[0] :
        {
          bornProvince: '',
          bornContry: '',
          socialSecurityNo: '',
          credentialType: '',
          credentialNumber: '',
          maritalStatus: '',
          nationality: '',
          phone: '',
          address: ''
        };
    });
  }

  @action
  public componentDidMount(): void {
    // 获取病人信息
    let params = {
      'criteria': {
        'pageSize': this.page.pageSize,
        'current': this.page.current,
        'sort': ''
      }
    };
    this.getPatients(params);

    // 请求可搜索条件list
    ruleStore.getFields().then(res => {
      this.searchList = res.filter(item => item.fieldDes !== '' && item.fieldDes !== '时间戳');
    });
  }

  public render(): JSX.Element {
    const rowSelection = {
      onChange: (selectedRowKeys, selectedRows) => {
        this.selectedData = selectedRows;
        this.selectedRowKeys = selectedRowKeys;
      },
      selectedRowKeys: this.selectedRowKeys.slice(0),
      type: ('checkbox' as 'checkbox' | 'radio')
    };
    const rowSelectionMerge = {
      onChange: (selectedRowKeys, selectedRows) => {
        this.selectedDataMerge = selectedRows;
        this.selectedRowKeysMerge = selectedRowKeys;
      },
      selectedRowKeys: this.selectedRowKeysMerge.slice(0),
      type: ('checkbox' as 'checkbox' | 'radio')
    };

    let box = document.querySelector('.patient-content');
    const y = box ? (box.clientHeight - 90) : 740;
    return (
      <div className='patient'>
        {this.mergeRecords ?
          <div className='patient-head'>
            <div className='patient-head-button-group'>
              <Button className='button-cancel'
                onClick={() => { this.mergeRecords = false; this.hideInfoPanel(); }}>取消</Button>
              <Button className='button-deactive' onClick={this.patientClickSplit}>拆分</Button>
            </div>
          </div>
          :
          <div className='patient-head'>
            <div className='patient-head-search-group'>
              <Select defaultValue='1' style={{ width: 120 }} onChange={this.handleChange}>
                <Option value='1'>全部内容</Option>
                <Option value='2'>人工合并</Option>
                <Option value='3'>自动合并</Option>
              </Select>
              <Select style={{ width: 120, marginLeft: 15 }}
                onChange={(value: any) => this.defaultSearchSelectChange(value)}
                value={this.selectCondition}>
                {
                  this.searchList.map(item => {
                    return <Option key={item.field} value={item.field}>{item.fieldDes}</Option>;
                  })
                }
              </Select>
              <Input className='search-input' placeholder='请输入搜索的内容' value={this.searchValue}
                onChange={(e: any) => this.defaultSearchInputChange(e)} />
              <Button className='search-button' type='primary' icon='search'
                onClick={this.defaultSearch}>搜索</Button>
              <span className='advanced-search' onClick={this.searchModalShow}>高级搜索</span>
              {
                this.getSearchModal()
              }
            </div>
            <div className='patient-head-button-group'>
              <span className='similar-data'>相似数据<i>2</i></span>
              <Button onClick={this.mergeClick} className='button-deactive'>合并</Button>
            </div>
          </div>
        }
        <div className='patient-content'>
          {this.mergeRecords ?
            <div className='patient-merge-table'>
              <div className='merge-head'>
                <Table
                  scroll={{ x: 1100, y: 550 }}
                  columns={this.mergeColumns}
                  dataSource={toJS(this.patientMergeList)}
                  pagination={false}
                />
              </div>
              <div className='merge-main'>
                <Table bordered
                  onRowClick={this.rowClickMerge}
                  rowSelection={rowSelectionMerge}
                  scroll={{ x: 1100, y: 350 }}
                  rowKey={(record: any) => { return record.id; }}
                  columns={this.mergeColumns}
                  dataSource={toJS(this.patientMerge)}
                  // rowSelection={rowSplitSelection}
                  // onRowClick={this.recordRowClick}
                  // scroll={{ x: 1100, y: 550 }}
                  // rowKey={(record: any) => { return record.comparisonId; }}
                  // columns={mergeColumns}
                  // dataSource={toJS(this.patientHistory)}
                  pagination={false}
                  showHeader={false}
                  expandedRowRender={(record: any) => this.mergeRowRender(record)}
                  expandIconAsCell={false}
                  expandIconColumnIndex={-1}
                  expandedRowKeys={this.state.mergeArray}
                />
              </div>
            </div>
            :
            <div className='patient-table'>
              <Table bordered
                onRowClick={this.rowClick}
                rowSelection={rowSelection}
                scroll={{ x: 1100, y: y }}
                rowKey={(record: any) => { return record.id; }}
                columns={this.columns}
                dataSource={toJS(this.patientList)}
                pagination={false}
              />
              <div>
                <Pagination
                  className='patient-pagination'
                  total={this.page.total}
                  defaultCurrent={this.page.current}
                  defaultPageSize={this.page.pageSize}
                  current={this.page.current}
                  showTotal={(total, range) => `第${Math.ceil(total / this.page.pageSize)}页 共${total} 条`}
                  onChange={this.pageChange}
                  onShowSizeChange={this.pageChange}
                  showSizeChanger
                  showQuickJumper
                />
              </div>
            </div>
          }
          <div className='patient-info'>
            <PatientInfo
              IPatientInfo={this.patientInfo}
            />
            <div className='patient-info-icon' onClick={this.hideInfoPanel}></div>
          </div>
        </div>
      </div>
    );

  }

  // 点击合并
  public mergeClick = () => {
    if (this.selectedRowKeys.length > 2) {
      message.warning('只能同时合并两条数据！');
      return false;
    }
    let mergePatient = {
      'sPatient': this.selectedData[0],
      'patient': this.selectedData[1]
    };
    patientStore.mergePatients(mergePatient).then(res => {
      if (res) {
        let params = {
          'criteria': {
            'pageSize': this.page.pageSize,
            'current': this.page.current,
            'sort': ''
          }
        };
        patientStore.getPatients(params).then(res2 => {
          this.page.total = res2.total;
          this.patientList = res2.patients;
          this.patientInfo = res2.patients[0];
          message.info('合并成功！');
        });
      } else {
        message.warning('合并失败！');
      }

    });
  }

  // 切换页码的函数
  public pageChange = (page: number, pageSize: number) => {
    this.page.pageSize = pageSize;
    this.page.current = page;
    let params = {
      'criteria': {
        'pageSize': this.page.pageSize,
        'current': this.page.current,
        'sort': ''
      }
    };
    this.getPatients(params);
  }

  // 规则
  public mergeRowRender = (record: any) => {
    const columns = [
      {
        'title': 'split',
        'dataIndex': 'split',
        'width': '40px',
        render: (text: any, record: any, index: number): JSX.Element => {
          return <span className='patient-split hide'>拆</span>;
        }
      },
      {
        'title': 'rule',
        'dataIndex': 'rule',
        render: (text: any, record: any, index: number): JSX.Element => {
          return <span>{'规则：'}<span className='rule-style'>{record.rule.rule}</span>{record.rule.ruleContent}</span>;
        }
      },
      {
        'title': 'datasource',
        'dataIndex': 'datasource',
        render: (text: any, record: any, index: number): JSX.Element => {
          return <span>{'数据来源：' + record.datasource}</span>;
        }
      }
    ];
    return (
      <Table
        onRowClick={this.rowClickMerge}
        columns={columns}
        dataSource={toJS(record.children)}
        pagination={false}
        showHeader={false}
        expandedRowRender={(record: any) => this.dataSourceRowRender(record)}
        expandIconAsCell={false}
        expandIconColumnIndex={-1}
        expandedRowKeys={this.state.dataSourceArray}
      />
    );
  }
  // 源数据
  public dataSourceRowRender = (record: any) => {
    const columns = [
      {
        'title': 'icon',
        'dataIndex': 'icon',
        'width': '40px'
      },
      {
        'title': 'EMPIID',
        'dataIndex': 'empiId',
        'width': 120
      }, {
        'title': 'Empi号码',
        'dataIndex': 'empiNumber',
        'width': 120
      }, {
        'title': '患者姓名',
        'dataIndex': 'name',
        'width': 120
      }, {
        'title': '性别',
        'dataIndex': 'sex',
        'width': 80
      }, {
        'title': '出生日期',
        'dataIndex': 'birthday',
        'width': 120
      }, {
        'title': '证件号码',
        'dataIndex': 'credentialNumber',
        'width': 160
      }, {
        'title': '医保号',
        'dataIndex': 'socialSecurityNo',
        'width': 120
      }, {
        'title': '合并日期',
        'dataIndex': 'updateAt',
        'width': 150
      }, {
        'title': '操作',
        'dataIndex': 'operate',
        'width': 150
      }
    ];
    return (
      <Table
        onRowClick={this.rowClickMerge}
        columns={columns}
        dataSource={toJS(this.patientHistory)}
        pagination={false}
        showHeader={false}
        expandedRowRender={(record: any) => this.mergeRowRender(record)}
        expandIconAsCell={false}
        expandIconColumnIndex={-1}
        expandedRowKeys={this.state.checkdataSourceArray}
      />
    );
  }
  // 查看源数据
  public viewRecords = (key: any) => {
    this.state.dataSourceArray[0] === key ?
      this.setState({
        dataSourceArray: [0]
      }) :
      this.setState({
        dataSourceArray: [key]
      });
    let checkdataSourceArray = [];
    this.patientHistory.forEach(item => {
      if (item.children) {
        checkdataSourceArray.push(item.key);
      }
    });
    this.setState({
      checkdataSourceArray: checkdataSourceArray
    });
    // 查看源数据
    // public viewRecords = (record: any) => {
    //   // 第一次点两下才渲染 bug
    //   if (this.state.expandArray.length === 0 || this.state.expandArray[0] !== record.comparisonId) {
    //     patientStore.getRawPatient(record.comparisonId).then(res => {
    //       this.rawPatient = [res];
    //       this.setState({
    //         expandArray: [record.comparisonId]
    //       });
    //     });
    //   } else {
    //     this.setState({
    //       expandArray: [0]
    // }
  }

  // 查看合并记录
  public viewMergeRecords = (record: any) => {
    this.hideInfoPanel();
    this.patientMergeList = [];
    this.mergeRecords = true;
    this.patientMergeList.push(record);
    let mergeArray = [];
    this.patientMerge.forEach(item => {
      if (item.children) {
        mergeArray.push(item.key);
      }
    });
    this.setState({
      mergeArray: mergeArray
    });
  }

  // 拆分
  public patientClickSplit = () => {
    this.searchModalFlag = true;
    this.modalParam = {
      title: '拆分',
      okText: '确定拆分',
      data: this.selectedDataMerge
    };
    console.log('拆分', this.selectedDataMerge);
  }

  // 信息栏显示隐藏
  public hideInfoPanel = () => {
    const pInfo = document.querySelector('.patient-info');
    const pTable = document.querySelector('.patient-table') ?
      document.querySelector('.patient-table') :
      document.querySelector('.patient-merge-table');

    pInfo.classList.contains('patient-info-hide') ?
      (pInfo.className = 'patient-info',
        pTable['style'].right = '257px') :
      (pInfo.className = 'patient-info patient-info-hide',
        pTable['style'].right = '63px');
  }

  // 病人列表点击选中行
  @action
  public rowClick = (record: any, index: number, event: any) => {
    let keyFlag = false;
    let keyIndex = -1;
    this.selectedRowKeys.forEach((item, ind) => {
      if (item === record['id']) {
        keyFlag = true;
        keyIndex = ind;
      }
    });
    if (keyFlag) {
      this.selectedRowKeys.splice(keyIndex, 1);
      this.selectedData.splice(keyIndex, 1);
    } else {
      this.selectedRowKeys.push(record['id']);
      this.selectedData.push(record);
    }
    const keyLen = this.selectedRowKeys.length;
    const btn = document.querySelector('.patient-head-button-group button');
    keyLen >= 2 ?
      btn.className = 'ant-btn button-active' :
      btn.className = 'ant-btn button-deactive';
  }
  // 合并页-点击选中行
  @action
  public rowClickMerge = (record: any, index: number, event: any) => {
    let data = record;
    this.patientMerge.map(item => {
      if (item.children && toJS(item.children[0].id) === record.id) {
        data = item;
      }
    });
    this.selectedRowKeysMerge = [data.id];
    this.selectedDataMerge = [data];
    const btn = document.querySelectorAll('.patient-head-button-group button')[1];
    this.selectedRowKeysMerge ?
      btn.className = 'ant-btn button-active' :
      btn.className = 'ant-btn button-deactive';
  }
  public defaultSearch = () => {
    let patient = {};
    patient[this.selectCondition] = this.searchValue;
    let params = {
      'criteria': {
        'pageSize': this.page.pageSize,
        'current': this.page.current,
        'sort': ''
      },
      'patient': patient
    };
    this.getPatients(params);
  }

  public defaultSearchSelectChange = (value: any) => {
    this.selectCondition = value;
  }

  public defaultSearchInputChange = (e: any) => {
    this.searchValue = e.target.value;
  }
  public handleChange = (value: any) => {
    console.log(value);
  }
  // 高级搜索弹出
  public searchModalShow = () => {
    this.searchModalFlag = true;
  }
  // 高级搜索modal
  public getSearchModal = () => {
    return (
      <Modal
        width={660}
        wrapClassName='vertical-center-modal'
        visible={this.searchModalFlag}
        title='高级搜索'
        onOk={this.searchOk}
        onCancel={this.searchCancel}
        cancelText='取消'
        okText='确定搜索'>
        <Form className='search-modal-form'>
          <Row>
            <Col span={11}>{this.getFormInput('EMPID', 'empiId', false)}</Col>
            <Col push={2} span={11}>{this.getFormInput('EMPI号码', 'empiNumber', false)}</Col>
          </Row>
          <Row>
            <Col span={11}>{this.getFormInput('患者姓名', 'name', false)}</Col>
            <Col push={2} span={11}>{this.getFormSelector('性别', 'sex', false)}</Col>
          </Row>
          <Row>
            <Col span={11}>{this.getFormDate('出生日期', 'birthday', false)}</Col>
            <Col push={2} span={11}>{this.getFormInput('医保号', 'medicalInsuranceNo', false)}</Col>
          </Row>
          <Row>
            <Col span={11}>{this.getFormInput('证件类型', 'credentialType', false)}</Col>
            <Col push={2} span={11}>{this.getFormInput('证件号码', 'credentialNumber', false)}</Col>
          </Row>
          <Row>
            <Col span={11}>{this.getFormInput('出生省份', 'bornProvince', false)}</Col>
            <Col push={2} span={11}>{this.getFormInput('出生县市', 'bornContry', false)}</Col>
          </Row>
          <Row>
            <Col span={11}>{this.getFormInput('社保卡号', 'socialSecurityNo', false)}</Col>
            <Col push={2} span={11}>{this.getFormInput('婚姻状况', 'maritalStatus', false)}</Col>
          </Row>
          <Row>
            <Col span={11}>{this.getFormInput('国籍', 'nationality', false)}</Col>
            <Col push={2} span={11}>{this.getFormInput('电话', 'phone', false)}</Col>
          </Row>
          <Row>
            <Col span={11}>{this.getFormInput('地址', 'address', false)}</Col>
          </Row>
        </Form>
      </Modal>
    );
  }

  public getFormInput = (labelName: string, props: string, required: boolean) => {
    const requiredText = required ? '* ' : '';
    const label = `${requiredText}${labelName}`;
    return <FormItem {...formLayout} label={label}
      className={required ? 'table-form-required' : ''}>
      <Input value={this.searchConditions[props]}
        onChange={(e) => this.searchInputChange(e, props)} />
    </FormItem>;
  }

  public getFormDate = (labelName: string, props: string, required: boolean) => {
    const requiredText = required ? '* ' : '';
    const label = `${requiredText}${labelName}`;
    let momentDate = null;
    if (this.searchConditions[props]) {
      momentDate = moment(this.searchConditions[props]);
    }
    return <FormItem {...formLayout} label={label}
      className={required ? 'table-form-required' : ''}>
      <DatePicker
        onChange={
          (date, datestring) => this.searchDateChange(date, datestring, props)
        }
        value={momentDate} />
    </FormItem>;
  }

  public getFormSelector = (labelName: string, props: string, required: boolean) => {
    const requiredText = required ? '* ' : '';
    const label = `${requiredText}${labelName}`;
    return <FormItem {...formLayout} label={label}
      className={required ? 'table-form-required' : ''}>
      <Select defaultValue='全部' onChange={(value: any) => this.searchSelectChange(value, props)}>
        <Option value='男'>男</Option>
        <Option value='女'>女</Option>
        <Option value='全部'>全部</Option>
      </Select>
    </FormItem>;
  }
  public searchInputChange = (e: any, props: string) => {
    this.searchConditions[props] = e.target.value;
  }
  public searchSelectChange = (value: any, props: string) => {
    if (this.searchConditions[props] === '全部') {
      this.searchConditions[props] = '';
    } else {
      this.searchConditions[props] = value;
    }
  }

  public searchDateChange = (date: any, datestring: any, props: any) => {
    if (date) {
      this.searchConditions[props] = new Date(date);
    } else {
      this.searchConditions[props] = null;
    }
  }

  // 高级搜索确定
  @action
  public searchOk = () => {
    this.searchModalFlag = false;
    if (this.searchConditions.sex === '全部') {
      this.searchConditions.sex = '';
    }
    let params = {
      'criteria': {
        'pageSize': this.page.pageSize,
        'current': this.page.current,
        'sort': ''
      },
      'patient': toJS(this.searchConditions)
    };
    this.getPatients(params);
  }

  // 高级搜索取消
  @action
  public searchCancel = () => {
    this.searchModalFlag = false;
  }

}
