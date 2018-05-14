import * as React from 'react';
import { Button } from 'antd';
import { IPatientInfo }  from './interface';
import './style.scss';

export class PatientInfo extends React.Component<{IPatientInfo}, {}> {
  constructor(props: any) {
    super(props);
  }
  public render(): JSX.Element {
    return <div className='patient-info-content'>
      <div className='patient-info-row'>
        <span>出生省份：</span>
        <span>{this.props.IPatientInfo.bornProvince}</span>
      </div>
      <div className='patient-info-row'>
        <span>出生县市：</span>
        <span>{this.props.IPatientInfo.bornContry}</span>
      </div>
      <div className='patient-info-row'>
        <span>社保卡号：</span>
        <span>{this.props.IPatientInfo.socialSecurityNo}</span>
      </div>
      <div className='patient-info-row'>
        <span>证件类型：</span>
        <span>{this.props.IPatientInfo.credentialType}</span>
      </div>
      <div className='patient-info-row'>
        <span>证件号码：</span>
        <span>{this.props.IPatientInfo.credentialNumber}</span>
      </div>
      <div className='patient-info-row'>
        <span>婚姻状况：</span>
        <span>{this.props.IPatientInfo.maritalStatus}</span>
      </div>
      <div className='patient-info-row'>
        <span>国籍：</span>
        <span>{this.props.IPatientInfo.nationality}</span>
      </div>
      <div className='patient-info-row'>
        <span>电话：</span>
        <span>{this.props.IPatientInfo.phone}</span>
      </div>
      <div className='patient-info-row'>
        <span>地址：</span>
        <span>{this.props.IPatientInfo.address}</span>
      </div>
    </div>;
  }

}
