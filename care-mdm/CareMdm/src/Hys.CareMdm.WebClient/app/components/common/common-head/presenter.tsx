import * as React from 'react';
import { ICommonHead } from './interface';
import { Dropdown, Button, Menu, Icon } from 'antd';
import logo from '../../../common/image/logo.png';
import './style.scss';

export class CommonHead extends React.Component<ICommonHead, {}> {
  constructor(props: ICommonHead) {
    super(props);
  }
  public render(): JSX.Element {
    const menu = (
      <Menu>
        <Menu.Item key='0'>张三</Menu.Item>
        <Menu.Item key='1'>李四</Menu.Item>
        <Menu.Item key='3'>王五</Menu.Item>
      </Menu>);
    return <div className='common-head'>
      <div className='common-icon'>
        <img src={logo} alt='云医院' title='云医院' />
      </div>
      {/* <div className='patient-name'>
        <Dropdown overlay={menu}>
          <a className='ant-dropdown-link'>
          张三
          <span className='ant-dropdown-icon'></span>
          </a>
        </Dropdown>
      </div> */}
    </div>;
  }
}
