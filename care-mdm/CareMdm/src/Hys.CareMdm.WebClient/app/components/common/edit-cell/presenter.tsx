import * as React from 'react';
import { IEditcell } from './interface';
import { Input, Checkbox, Select } from 'antd';
import { observer } from 'mobx-react';
import { observable, action } from 'mobx';
import './style.scss';
const Option = Select.Option;
const dataTypeList = ['bigint', 'int', 'smallint', 'tinyint', 'bit', 'float', 'datetime', 'text', 'ntext'
  , 'char(10)', 'varchar(10)', 'nchar(10)', 'nvarchar(10)'];

@observer
export class EditableCell extends React.Component<IEditcell, {}> {
  @observable public editable: boolean;
  @observable public value: any;
  constructor(props: IEditcell) {
    super(props);
    this.editable = this.props.editable;
    this.value = this.props.value;
  }

  public componentWillReceiveProps(nextProps: any): void {
    this.value = nextProps.value;
    this.editable = nextProps.editable;

  }

  @action
  public inputChange = (e: any) => {
    this.value = e.target.value;
    this.props.onChange(this.value);
  }

  @action
  public checkChange = (e: any) => {
    this.value = e.target.checked;
    this.props.onChange(this.value);
  }
  @action
  public handleChange = (value: any) => {
    this.value = value;
    this.props.onChange(this.value);
  }
  public render(): JSX.Element {
    return (
      <div>
        {
          this.editable ?
            <div>
              {this.props.selectAble ? <Select
                defaultValue={this.value}
                onChange={this.handleChange}
                style={{ width: 150 }}
              >
                {this.props.selectList.map(item => {
                  return <Option key={item}> {item}</Option>;
                })}
              </Select> : (
                  this.props.dataType === 'boolean' ?
                    <Checkbox checked={this.value} onChange={(e) => this.checkChange(e)} />
                    :
                    <Input value={this.value} onChange={(e) => this.inputChange(e)} />)
              }
            </div>
            :
            <div className='editable-row-text'>
              {
                this.props.dataType === 'boolean' ?
                  <Checkbox checked={this.value} />
                  :
                  this.props.value
              }
            </div>
        }
      </div>
    );
  }
}
