import * as React from 'react';
import { Table, Button, Input, Icon, Col, message } from 'antd';
import { IPatient, IPatientData, IRawPatient } from './interface';
// import { TableData } from '../source';
import { observer } from 'mobx-react';
import { observable, action, toJS } from 'mobx';
import { AddPatient } from './add-patient';
import { PatientStore } from '../../store';
import './style.scss';

const patientStore = new PatientStore();
let total = 0;
let sort = '';
@observer
export class Patient extends React.Component<IPatient, {}> {
  @observable public name: string;
  @observable public empiId: string;
  @observable public empiNumber: string;
  @observable public phone: string;
  @observable public socialSecurityNo: string;
  @observable public medicalInsuranceNo: string;
  @observable public pagination: any;
  @observable public loading: boolean;
  @observable public showFlag: boolean;
  @observable public patients: Array<IPatientData>;
  // @observable public sources: Array<TableData>;
  @observable public sources: Array<any>;
  public rawPatient: IRawPatient;
  @observable public testFlag: boolean;
  public birthday: any;
  constructor(props: IPatient) {
    super(props);
    this.name = '';
    this.empiId = '';
    this.empiNumber = '';
    this.phone = '';
    this.socialSecurityNo = '';
    this.medicalInsuranceNo = '';
    this.loading = false;
    this.testFlag = false;
    this.pagination = {
      current: 1,
      pageSize: 10,
      total: total
    };
    this.patients = [];
    this.sources = [];
    this.rawPatient = {
      sourceId: null,
      sourceVersion: null,
      sourcePatientId: '',
      name: '',
      sex: '',
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
      address: '',
      createdAt: null,
      sourceCreatedAt: null
    };
  }

  public componentDidMount(): void {
    this.getPatients();
    this.props.getSources().then(res => {
      this.sources = res;
    });
  }

  public render(): JSX.Element {
    const columns = [{
      title: 'EMPIID',
      dataIndex: 'empiId',
      width: '12%',
      sorter: true
    }, {
      title: 'EMPINumber',
      dataIndex: 'empiNumber',
      sorter: true,
      width: '10%'
    }, {
      title: '名字',
      dataIndex: 'name',
      sorter: true,
      width: '8%'
    }, {
      title: '性别',
      dataIndex: 'sex',
      sorter: true,
      width: '5%'
    }, {
      title: '出生日期',
      dataIndex: 'birthday',
      render: (text, record, index) => {
        return <div>{text.toLocaleString().slice(0, 10)}</div>;
      },
      sorter: true,
      width: '10%'
    }, {
      title: '证件类型',
      dataIndex: 'credentialType',
      sorter: true,
      width: '7%'
    }, {
      title: '证件号码',
      dataIndex: 'credentialNumber',
      sorter: true,
      width: '10%'
    }, {
      title: '医保号',
      dataIndex: 'medicalInsuranceNo',
      sorter: true,
      width: '10%'
    }, {
      title: '社保号',
      dataIndex: 'socialSecurityNo',
      sorter: true,
      width: '10%'
    }, {
      title: '联系方式',
      dataIndex: 'phone',
      sorter: true,
      width: '7%'
    }, {
      title: '联系地址',
      dataIndex: 'address',
      sorter: true,
      width: '10%'
    }];
    return <div className='patient'>
      <div className='patient-body'>
        <div className='operate-group'>
          <div className='button-group'>
            <Button onClick={this.addPatient}>新增</Button>
          </div>
          <div className='search-group'>
            <Col span={22}>
              <label>EMPIID</label>
              <Input onChange={(e) => this.searchChange(e, 'empiId')} />
              <label>EMPINumber</label>
              <Input onChange={(e) => this.searchChange(e, 'empiNumber')} />
              <label>名字</label>
              <Input onChange={(e) => this.searchChange(e, 'name')} />
              <label>医保号</label>
              <Input onChange={(e) => this.searchChange(e, 'medicalInsuranceNo')} />
              <label>社保号</label>
              <Input onChange={(e) => this.searchChange(e, 'socialSecurityNo')} />
              <label>联系方式</label>
              <Input onChange={(e) => this.searchChange(e, 'phone')} />
            </Col>
            <Col span={2}>
              <Button icon='search' shape='circle' onClick={this.getPatients}></Button>
            </Col>
          </div>
        </div>
        <Table rowKey='id'
          columns={columns}
          dataSource={this.patients.slice(0)}
          loading={this.loading}
          pagination={this.pagination}
          onChange={this.tableChange}
          scroll={{ y: 400 }}
        >
        </Table>
      </div>
      {this.testFlag ? <div style={{ opacity: 0 }}>1</div> : ''}
      <AddPatient
        showFlag={this.showFlag}
        sources={this.sources}
        cancel={this.modalCancel}
        ok={this.modalOK}
        rawPatient={this.rawPatient}
        dateChange={this.dateChange}
        sourceChange={this.selectSourceChange}
        inputChange={this.inputChange}
        sexChange={this.selectSexChange}
        birthday={this.birthday} />
    </div>;
  }

  @action
  public addPatient = () => {
    this.rawPatient = {
      sourceId: null,
      sourceVersion: null,
      sourcePatientId: '',
      name: '',
      sex: '',
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
      address: '',
      createdAt: null,
      sourceCreatedAt: null
    };
    this.birthday = null;
    this.showFlag = true;
  }

  @action
  public dateChange = (date: any, dateString: string) => {
    this.rawPatient.birthday = new Date(dateString);
    this.birthday = date;
    this.testFlag = !this.testFlag;
  }

  @action
  public selectSourceChange = (value: string) => {
    this.rawPatient.sourceId = parseInt(value, 0);
    this.testFlag = !this.testFlag;
  }

  @action
  public selectSexChange = (value: string) => {
    this.rawPatient.sex = value;
    this.testFlag = !this.testFlag;
  }

  @action
  public inputChange = (props: string, e: any) => {
    this.rawPatient[props] = e.target.value;
    this.testFlag = !this.testFlag;
  }

  @action
  public modalCancel = () => {
    this.showFlag = false;
  }

  public modalOK = () => {
    if (!this.rawPatient.sourceId) {
      message.info('请选择数据源！');
      return false;
    }
    if (!this.rawPatient.sourcePatientId) {
      message.info('请填写病人ID！');
      return false;
    }
    if (this.rawPatient.birthday > new Date()) {
      message.info('出生日期不能大于今天！');
      return false;
    }
    this.rawPatient.createdAt = new Date();
    this.rawPatient.sourceCreatedAt = new Date();
    this.sources.forEach(item => {
      if (item.id === this.rawPatient.sourceId) {
        this.rawPatient.sourceVersion = item.latestVersion;
      }
    });
    this.props.addPatient(this.rawPatient).then(res => {
      if (res !== 0) {
        let param = {
          criteria: {
            pageSize: this.pagination.pageSize,
            current: this.pagination.current,
            sort: sort
          }
        };
        this.loading = false;
        patientStore.getPatients(param).then(res => {
          this.pagination.total = res.total;
          this.patients = res.patients ? res.patients : [];
          message.info('新增成功！');
        });
      } else {
        message.info('新增失败！');
      }
    });
    this.showFlag = false;
  }

  @action
  public tableChange = (pagination: any, filters: any, sorter: any) => {
    this.loading = true;
    if (sorter.order === 'ascend') {
      sort = sorter.field + ' asc';
    } else {
      if (sorter.order === 'descend') {
        sort = sorter.field + ' desc';
      } else {
        sort = '';
      }
    }
    let criteria = {
      pageSize: pagination.pageSize,
      current: pagination.current,
      sort: sort
    };
    let param = {
      name: this.name,
      empiId: this.empiId,
      empiNumber: this.empiNumber,
      medicalInsuranceNo: this.medicalInsuranceNo,
      socialSecurityNo: this.socialSecurityNo,
      phone: this.phone,
      criteria: criteria
    };
    patientStore.getPatients(param).then(res => {
      this.loading = false;
      this.pagination.current = pagination.current;
      this.pagination.total = res.total;
      this.patients = res.patients ? res.patients : [];
    });
  }

  public searchChange = (e: any, index: string) => {
    this[index] = e.target.value;
  }

  public getPatients = () => {
    this.loading = true;
    let param = {
      criteria: {
        pageSize: this.pagination.pageSize,
        current: this.pagination.current,
        sort: sort
      },
      name: this.name,
      empiId: this.empiId || null,
      empiNumber: this.empiNumber,
      medicalInsuranceNo: this.medicalInsuranceNo,
      socialSecurityNo: this.socialSecurityNo,
      phone: this.phone

    };
    patientStore.getPatients(param).then(res => {
      this.loading = false;
      this.pagination.total = res.total;
      this.patients = res.patients ? res.patients : [];
    });
  }

}
