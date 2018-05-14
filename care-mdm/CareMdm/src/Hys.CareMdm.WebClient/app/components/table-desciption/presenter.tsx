import * as React from 'react';
import { ITableDescription } from './interface';
import moment from 'moment';
import { Row, Col, Checkbox, Form } from 'antd';
import './style.scss';

const FormItem = Form.Item;
const formLayout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 16 }
};

const formLayout2 = {
    labelCol: { span: 2 },
    wrapperCol: { span: 20 }
};

export class TableDescription extends React.Component<ITableDescription, {}> {
    constructor(props: ITableDescription) {
        super(props);
    }

    public shouldComponentUpdate(nextProps: any, nextState: any): boolean {
        if (nextProps.tableDescription === this.props.tableDescription) {
            return false;
        } else {
            return true;
        }
    }

    public render(): JSX.Element {
        return (
            <div className='table-description'>
                <Form className='table-description-content'>
                    <Row>
                        <Col span={9}>{this.getFormText('标准代码', 'standardCode', formLayout)}</Col>
                        <Col span={8}>{this.getFormText('中文名称', 'standardCnName', formLayout)}</Col>
                        <Col span={7}>{this.getFormText('中文简称', 'name', formLayout)}</Col>
                    </Row>
                    <Row>
                        <Col span={9}>{this.getFormText('英文名称', 'standardEnName', formLayout)}</Col>
                        <Col span={8}>{this.getFormText('英文简称', 'code', formLayout)}</Col>
                        <Col span={7}>{this.getCheck()}</Col>
                    </Row>
                    <Row>
                        <Col span={9}>{this.getTime('启用时间', 'activeTime')}</Col>
                        <Col span={8}>{this.getTime('停用时间', 'deactiveTime')}</Col>
                    </Row>
                    <Row>
                        {this.getFormText('主数据描述', 'description', formLayout2)}
                    </Row>
                    <Row>
                        {this.getFormText('备注', 'comments', formLayout2)}
                    </Row>
                </Form>
                <div className='operate-descript operate-container'>
                    {/* <div className='common-operate'>
                        <div className='common-operate-background'></div>导入
                    </div> */}
                    <div className='common-operate' onClick={this.props.update}>
                        <div className='common-operate-background'></div>修改
                    </div>
                </div>
            </div>
        );
    }

    public getFormText = (labelName: string, props: string, formLayout: any) => {
        return <FormItem {...formLayout} label={labelName}>
            <span>{this.props.tableDescription[props]}</span>
        </FormItem>;
    }

    public getCheck = () => {
        return <FormItem {...formLayout} label='是否启用'>
            <Checkbox checked={this.props.tableDescription.isActive} />
        </FormItem>;
    }

    public getTime = (labelName: string, props: string) => {
        let text = '';
        if (this.props.tableDescription[props]) {
            text = moment(this.props.tableDescription[props]).format('L');
        }
        return <FormItem {...formLayout} label={labelName}>
            <span>{text}</span>
        </FormItem>;
    }
}
