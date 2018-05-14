import * as React from 'react';
import { ICommonHead } from './interface';
import './style.scss';

export class CommonHead extends React.Component <{}, {}> {
  constructor(props: {}) {
    super(props);
  }
  public render(): JSX.Element {
    return <div className='common-head'>
      {/* <span className='common-head-logo'></span> */}
    </div>;
  }
}
