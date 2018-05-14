import * as React from 'react';
import { IEditcell } from './interface';
import { Input, Select } from 'antd';
import { observer } from 'mobx-react';
import { observable, action } from 'mobx';
import './style.scss';

const Option = Select.Option;
@observer
export class EditableCell extends React.Component<IEditcell, {}> {
  @observable public editable: boolean;
  public fields: Array<string>;
  @observable public value: string;
  @observable public cacheValue: string;
  constructor(props: IEditcell) {
    super(props);
    this.editable = this.props.editable;
    this.fields = this.props.fields ? this.props.fields : [];
    this.value = this.props.value;
  }

  public componentWillReceiveProps(nextProps: any): void {
    if (nextProps.editable !== this.editable) {
      this.editable = nextProps.editable;
      if (nextProps.status === 'cancel') {
        this.cacheValue = this.props.value;
      }
    }
    if (nextProps.status && nextProps.status !== this.props.status) {
      if (nextProps.status === 'save') {
        this.props.onChange(this.value);
      } else if (nextProps.status === 'cancel') {
        this.value = this.cacheValue;
        this.props.onChange(this.cacheValue);
      }
    }
  }
  // public shouldComponentUpdate(nextProps: any, nextState: any): boolean {
  //   return nextProps.editable !== this.editable || nextState.value !== this.value;
  // }
  @action
  public handleChange = (e: any) => {
    const value = e.target.value;
    this.value = value;
  }

  @action
  public selectChange = (value: string) => {
    this.value = value;
  }
  public render(): JSX.Element {
    return (
      <div>
        {
          this.editable ?
            <div>
              {
                this.fields.length > 0 ?
                  <Select size='large' showSearch defaultValue={this.value}
                    onChange={this.selectChange}>
                    {
                      this.fields.map((item) => {
                        return <Option key={item} value={item}>{item}</Option>;
                      })
                    }
                  </Select> :
                  <Input
                    value={this.value}
                    onChange={e => this.handleChange(e)}
                  />
              }
            </div>
            :
            <div className='editable-row-text'>
              {this.value.toString() || ' '}
            </div>
        }
      </div>
    );
  }
}
