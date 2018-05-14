import * as React from 'react';
import { Route, RouteProps, RouteComponentProps } from 'react-router-dom';

export interface LazyRouteProps extends RouteProps {
  load: () => Promise<any>;
  selector: (sel: any) => any;
}

export class LazyRoute extends React.Component<LazyRouteProps, { component: any; }> {
  constructor(props: LazyRouteProps) {
    super(props);
    this.state = {
      component: null
    };
  }
  public componentWillMount(): void {
    this.load(this.props);
  }

  public componentWillReceiveProps(nextProps: LazyRouteProps): void {
    this.load(nextProps);
  }
  public render(): JSX.Element {
    let { load, component, selector, render, ...routeProps } = this.props;
    return this.state.component ?
      <Route
        component={selector(this.state.component)}
        {...routeProps} /> :
      null;
  }

  private load(props: LazyRouteProps): void {
    this.setState({
      component: null
    });
    props.load().then(comp => {
      this.setState({
        component: comp
      });
    });
  }
}
