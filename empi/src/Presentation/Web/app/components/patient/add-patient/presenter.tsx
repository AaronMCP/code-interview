import * as React from 'react';
import { IAddPatient } from './interface';
import { Modal, Input, Form, Select, DatePicker } from 'antd';
import { observable, action, toJS } from 'mobx';
import { observer } from 'mobx-react';
import './style.scss';

const FormItem = Form.Item;
const Option = Select.Option;
const formLayout = {
  labelCol: { span: 6 },
  wrapperCol: { span: 14 }
};

export class AddPatient extends React.Component<IAddPatient, {}> {
  constructor(props: IAddPatient) {
    super(props);
  }

  public render(): JSX.Element {
    return <Modal
      closable={false}
      visible={this.props.showFlag}
      onCancel={this.props.cancel}
      onOk={this.props.ok}
    >
      <Form className='add-patient-form'>
        {this.getSourceSelect()}
        {this.getFormInput('病人ID', 'sourcePatientId')}
        {this.getFormInput('姓名', 'name')}
        {this.getSexSelect()}
        {this.getBirthday()}
        {this.getFormInput('证件类型', 'credentialType')}
        {this.getFormInput('证件号', 'credentialNumber')}
        {this.getFormInput('省份', 'bornProvince')}
        {this.getFormInput('国家', 'bornContry')}
        {this.getFormInput('医保号', 'medicalInsuranceNo')}
        {this.getFormInput('社保号', 'socialSecurityNo')}
        {this.getFormInput('民族', 'nationality')}
        {this.getFormInput('婚姻', 'maritalStatus')}
        {this.getFormInput('电话', 'phone')}
        {this.getFormInput('地址', 'address')}
      </Form>
    </Modal>;
  }

  public getSourceSelect = () => {
    return <FormItem {...formLayout} label='数据源'>
      <Select onChange={this.props.sourceChange}
      value={this.props.rawPatient.sourceId ? this.props.rawPatient.sourceId.toString() : ''}>
        {
          this.props.sources ?
            this.props.sources.map(item => {
              return <Option key={item.id} value={item.id.toString()}>{item.name}</Option>;
            }) : ''
        }
      </Select>
    </FormItem>;
  }

  public getSexSelect = () => {
    return <FormItem {...formLayout} label='性别'>
      <Select onChange={this.props.sexChange} value={this.props.rawPatient.sex}>
        <Option key='0' value='男'>男</Option>
        <Option key='1' value='女'>女</Option>
      </Select>
    </FormItem>;
  }

  public getFormInput = (labelName: string, props: string) => {
    return <FormItem {...formLayout} label={labelName}>
      <Input value={this.props.rawPatient[props]} onChange={(e) => this.props.inputChange(props, e)} />
    </FormItem>;
  }

  public getBirthday = () => {
    return <FormItem {...formLayout} label='生日'>
      <DatePicker onChange={this.props.dateChange} value={this.props.birthday} />
    </FormItem>;
  }
}
