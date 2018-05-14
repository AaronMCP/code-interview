import * as React from 'react';
import { Link } from 'react-router-dom';
import { Menu, Icon } from 'antd';
import { CommonHead } from '../common/common-head';
import { TableGroupStore } from '../../store';
import { observer } from 'mobx-react';
import { observable } from 'mobx';
import './style.scss';

const tableGroupStore = new TableGroupStore();
@observer
export class LeftBar extends React.Component<{ location: string; }, {}> {
  @observable public tabName: Array<any>;
  constructor(props: { location: string; }) {
    super(props);
  }

  public componentDidMount(): void {
    tableGroupStore.getAllTableGroups().then(res => {
      if (res) {
        this.tabName = res.result;
      }
    });
  }

  public render(): JSX.Element {
    const headerText = ['张三', '李四', '王五'];
    const tabName = [{
      'id': 1,
      'name': '主数据管理'
    }, {
      'id': 2,
      'name': '元数据管理'
    }, {
      'id': 3,
      'name': '交互服务'
    }, {
      'id': 4,
      'name': '日志检测'
    }];
    return <div>
      <CommonHead headText={headerText} />
      <ul className='core-nav'>
        <li className={this.props.location === '/mdm' ? 'active' : ''}>
          <Link to='/mdm'>{tabName[0].name}</Link>
        </li>
        <li className={this.props.location === '/meta' ? 'active' : ''}>
          <Link to='/meta'>{tabName[1].name}</Link>
        </li>
        <li className={this.props.location === '/users' ? 'active' : ''}>
          <Link to='/users'>{tabName[2].name}</Link>
        </li>
        <li className={this.props.location === '/about' ? 'active' : ''}>
          <Link to='/about'>{tabName[3].name}</Link>
        </li>
      </ul>
    </div>;
  }
}
