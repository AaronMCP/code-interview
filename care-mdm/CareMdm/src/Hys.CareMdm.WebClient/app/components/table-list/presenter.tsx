import * as React from 'react';
import { ITableList } from './interface';
import { Button, Modal, Popconfirm, Input, Row, Col, Form, DatePicker, Checkbox } from 'antd';
import moment from 'moment';
import './style.scss';

const FormItem = Form.Item;
const { TextArea  } = Input;
const formLayout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 18 }
};

export class TableList extends React.Component<ITableList, {}> {
    constructor(props: ITableList) {
        super(props);
    }

    public render(): JSX.Element {
        let title = '';
        if (this.props.addListFlag) {
            title = '新增' + this.props.groupText + '内容';
        } else {
            title = '修改' + this.props.groupText + '内容';
        }
        return (
            <Modal
                width={700}
                title={title}
                visible={this.props.modalListFlag}
                onCancel={this.props.onCancel}
                onOk={this.props.onOk}
                cancelText='取消'
                okText='确定'>
                <Form className='table-menu-from'>
                    <Row>
                        <Col span={11}>{this.getFormInput('标准代码', 'standardCode', false)}</Col>
                        <Col push={2} span={11}>{this.getFormInput('中文名称', 'standardCnName', false)}</Col>
                    </Row>
                    <Row>
                        <Col span={11}>{this.getFormInput('中文简称', 'name', true)}</Col>
                        <Col push={2} span={11}>{this.getFormInput('英文名称', 'standardEnName', false)}</Col>
                    </Row>
                    <Row>
                        <Col span={11}>{this.getFormInput('英文简称', 'code', true)}</Col>
                        <Col push={2} span={11}>{this.getCheckbox()}</Col>
                        {/* <Col span={12}>{this.getDateInput('启用时间', 'activeTime')}</Col> */}
                    </Row>
                    <Row>
                        {/* <Col span={12}>{this.getDateInput('停止时间', 'deactiveTime')}</Col> */}
                    </Row>
                    {this.getDescription()}
                    {this.getComments()}
                </Form>
            </Modal>
        );
    }

    public getFormInput = (labelName: string, props: string, required: boolean) => {
        const requiredText = required ? '* ' : '';
        const label = `${requiredText}${labelName}`;
        return <FormItem {...formLayout} label={label}
        className={required ? 'table-form-required' : ''}>
            <Input value={this.props.tableListData[props]} onChange={(e) => this.props.inputChange(props, e)} />
        </FormItem>;
    }

    public getCheckbox = () => {
        return <FormItem {...formLayout} label='是否启用'>
        <Checkbox checked={this.props.tableListData.isActive}
            onChange={(e) => this.props.checkChange(e)} />
    </FormItem>;
    }

    public getDateInput = (labelName: string, props: string) => {
        let momentDate = moment(new Date());
        if (this.props.tableListData[props]) {
            momentDate = moment(this.props.tableListData[props]);
        }
        return <FormItem {...formLayout} label={labelName}>
            <DatePicker onChange={(date, datestring) => this.props.dateChange(date, datestring, props)}
            value={momentDate} />
        </FormItem>;
    }

    public getDescription = () => {
        const formLayout2 = {
            labelCol: { span: 3 },
            wrapperCol: { span: 21 }
        };
        return <FormItem {...formLayout2} label='数据描述'>
            <TextArea value={this.props.tableListData.description} rows={4}
            onChange={(e) => this.props.inputChange('description', e)} />
        </FormItem>;
    }

    public getComments = () => {
        const formLayout3 = {
            labelCol: { span: 3 },
            wrapperCol: { span: 21 }
        };
        return <FormItem {...formLayout3} label='备注'>
            <TextArea value={this.props.tableListData.comments} rows={4}
            onChange={(e) => this.props.inputChange('comments', e)} />
        </FormItem>;
    }
}
