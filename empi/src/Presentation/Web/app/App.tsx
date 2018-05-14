import { PatientContainer } from './components/patient/container';
import * as React from 'react';
import { Switch } from 'react-router-dom';
import './common/style/common.scss';
import { CommonHead } from './components/common/common-head';
import { LeftBar } from './components/left-bar';
import { LazyRoute } from './common/lazy-route';

export class App extends React.Component<{}, {}> {
  constructor(props: {}) {
    super(props);
  }

  public render(): JSX.Element {
    let { location } = this.props as any;
    return <div className='index'>
      <CommonHead/>
      <LeftBar location={location.pathname} />
      <main className='main-body'>
        <Switch>
          <LazyRoute exact path='/'
            load={() => import('./components/Home')}
            selector={({ Home }) => Home} />
          <LazyRoute path='/rules'
            load={() => import('./components/rules')}
            selector={({ Rule }) => Rule} />
          <LazyRoute path='/source'
            load={() => import('./components/source')}
            selector={({ Source }) => Source} />
          <LazyRoute path='/patient'
            load={() => import('./components/patient2')}
            selector={({ Patient }) => Patient} />
        </Switch>
      </main>
    </div>;
  }
}
