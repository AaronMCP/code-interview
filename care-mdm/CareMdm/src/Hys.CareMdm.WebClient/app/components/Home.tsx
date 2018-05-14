import * as React from 'react';
import httpProxy from '../common/http-proxy';
import { Redirect } from 'react-router';

export class Home extends React.Component<{}, {}> {
    constructor(props: {}) {
        super(props);
    }
    public render(): JSX.Element {
        return <Redirect to='/mdm' />;
    }
}
