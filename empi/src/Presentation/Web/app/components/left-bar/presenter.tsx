import * as React from 'react';
import { Link } from 'react-router-dom';

export class LeftBar extends React.Component<{ location: string; }, {}> {
  constructor(props: { location: string; }) {
    super(props);
  }

  public render(): JSX.Element {
    return <ul className='core-nav'>
      <li className={this.props.location === '/patient' ? 'active' : ''}>
        <Link to='/patient'>
          <span className='patient-span'></span>
          <span>列表</span>
        </Link>
      </li>
      <li className={this.props.location === '/rules' ? 'active' : ''}>
        <Link to='/rules'>
          <span className='rules-span'></span>
          <span>规则设定</span>
        </Link>
      </li>
      <li className={this.props.location === '/source' ? 'active' : ''}>
        <Link to='/source'>
          <span className='source-span'></span>
          <span>优先级设定</span>
        </Link>
      </li>
    </ul>;
  }
}
