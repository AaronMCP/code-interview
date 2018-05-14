import * as React from 'react';
import { Switch } from 'react-router-dom';

import './common/style/common.scss';
import { LeftBar } from './components/left-bar';
import { LazyRoute } from './common/lazy-route';

export class App extends React.Component<{}, {}> {
  constructor(props: {}) {
    super(props);
  }

  public render(): JSX.Element {
    let { location } = this.props as any;
    return <div className='index'>
      <LeftBar location={location.pathname} />
      <main className='main-body'>
        <Switch>
          <LazyRoute path='/mdm'
            load={() => import('./components/main-data')}
            selector={({ MainData }) => MainData} />
          <LazyRoute exact path='/'
            load={() => import('./components/Home')}
            selector={({ Home }) => Home} />
          <LazyRoute path='/about'
            load={() => import('./components/About')}
            selector={({ About }) => About} />
          <LazyRoute path='/users'
            load={() => import('./components/Users')}
            selector={({ Users }) => Users} />
          <LazyRoute path='/meta'
            load={() => import('./components/meta-data')}
            selector={({ MetaData }) => MetaData} />
        </Switch>
      </main>
    </div>;
  }
}
