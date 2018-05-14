import * as React from 'react';
import { IRuleData, IRuleDetailData } from './interface';
import { Button, Table, Popconfirm, Modal, Select, Form, Input, message, InputNumber } from 'antd';
import { RuleStore } from '../../store';
import { observer } from 'mobx-react';
import { observable, action, toJS, runInAction } from 'mobx';
import './style.scss';

const ruleStore = new RuleStore();
const FormItem = Form.Item;
const confirm = Modal.confirm;
const formLayout = {
  labelCol: { span: 6 },
  wrapperCol: { span: 16 }
};
const Option = Select.Option;

@observer
export class Rule extends React.Component<{}, {}> {
  @observable public addFlag: boolean;
  @observable public modalFalg: boolean;
  @observable public historyFlag: boolean;
  @observable public updateRuleData: any;
  // 可选择的规则内容集合
  @observable public ruleDetailList: Array<IRuleDetailData>;
  // 所有规则集合
  @observable public ruleList: Array<IRuleData>;
  // 规则历史
  @observable public historyRuleList: Array<IRuleData>;
  constructor(props: {}) {
    super(props);
    this.addFlag = false;
    this.modalFalg = false;
    this.historyFlag = false;
    this.ruleDetailList = [];
    this.ruleList = [];
    this.historyRuleList = [];
    this.updateRuleData = {
      same: null,
      version: null,
      ruleDetails: [],
      priority: null
    };
  }

  // 初始化请求数据
  @action
  public componentDidMount(): void {
    // 请求规则内容list
    ruleStore.getFields().then(res => {
      this.ruleDetailList = res.filter(item => item.fieldDes !== '' && item.fieldDes !== '时间戳');
    });
    // 请求规则list
    ruleStore.getRules().then(res => {
      this.ruleList = res;
    });
  }

  public render(): JSX.Element {
    // 表格column
    const columns = [
      {
        title: '规则类型',
        dataIndex: 'same',
        width: '135px',
        render: (text: any, record: any, index: number): JSX.Element => {
          if (text === true) {
            return <span className='rule-type same'>相同</span>;
          } else {
            return <span className='rule-type no-same'>相似</span>;
          }
        }
      }, {
        title: '最新版本',
        dataIndex: 'latestVersion',
        width: '90px'
      }, {
        title: '规则内容',
        dataIndex: 'ruleDetails',
        width: '360px',
        render: (text: any, record: any, index: number): JSX.Element => {
          let arr = [];
          for (let i = 0; i < this.ruleDetailList.length; i++) {
            for (let j = 0; j < record.ruleDetails.length; j++) {
              if (this.ruleDetailList[i].field === record.ruleDetails[j].field) {
                arr.push(this.ruleDetailList[i].fieldDes);
              }
            }
          }
          return <span style={{ float: 'left' }}>{arr.join('+')}</span>;
        }
      }, {
        title: '优先级',
        dataIndex: 'priority',
        width: '100px',
        render: (text: any, record: any, index: number): JSX.Element => {
          if (record.same) {
            return <span className='same-rule-priority'>{text}</span>;
          } else {
            return <span className='no-same-rule-priority'>{text}</span>;
          }
        }
      }, {
        title: '操作',
        dataIndex: 'operate',
        width: '301px',
        render: (text: any, record: any, index: number): JSX.Element => {
          return <div className='rule-table-operate'>
            <span className='rule-history' onClick={() => { this.lookHistory(record); }}> 历史</span >
            <span className='rule-edit' onClick={() => { this.editRule(record); }}>编辑</span>
          </div >;
        }
      }
    ];
    let box = document.querySelector('.rules-detail');
    const y = box ? (box.clientHeight - 42) : 440;
    return (
      <div className='rules'>
        <div className='operate-container'>
          <Button onClick={this.addRule}>新增</Button>
        </div>
        <div className='rules-detail'>
          <Table bordered
            scroll={{ y }}
            rowKey={(record: any) => { return record.id; }}
            pagination={false}
            columns={columns}
            dataSource={toJS(this.ruleList)}
          />
          {this.getRuleModal()}
          {this.getHistoryModal()}
        </div>
      </div>);
  }

  //#region 新增、修改规则、查看历史
  // 新增规则modal
  public getRuleModal = () => {
    return <Modal
      width='376px'
      title={this.addFlag ? '新增规则' : '修改规则'}
      okText={this.addFlag ? '确定添加' : '确定保存'}
      cancelText='取消'
      visible={this.modalFalg}
      onOk={this.okEdit}
      onCancel={this.cancelEdit}
    >
      <Form>
        {this.getRuleTypeSelect()}
        {this.getRuleDetailSelect()}
        <FormItem {...formLayout} label='* 优先级' className='form-required'>
          <InputNumber min={1} value={this.updateRuleData.priority ? this.updateRuleData.priority : ''}
            onChange={(value) => this.inputNunberChange(value, 'priority')} />
        </FormItem>
      </Form>
    </Modal>;
  }

  // 规则类型select
  public getRuleTypeSelect = () => {
    return <FormItem {...formLayout} label='规则类型'>
      <Select value={this.updateRuleData.same ? this.updateRuleData.same : ''}
        onChange={(value) => this.selectChange(value, 'same')}>
        <Option key='相同'>相同</Option>
        <Option key='相似'>相似</Option>
      </Select>
    </FormItem>;
  }

  // 规则内容select
  public getRuleDetailSelect = () => {
    return <FormItem {...formLayout} label='* 规则内容' className='form-required'>
      <Select mode='tags' value={this.updateRuleData.ruleDetails ? toJS(this.updateRuleData.ruleDetails) : []}
        onChange={(value) => this.selectChange(value, 'ruleDetails')}>
        {
          this.ruleDetailList.map(item => {
            return <Option key={item.field}>{item.fieldDes}</Option>;
          })
        }
      </Select>
    </FormItem>;
  }

  // 历史modal
  public getHistoryModal = () => {
    // column
    const columns = [
      {
        title: '规则类型',
        dataIndex: 'same',
        width: '120px',
        render: (text: any, record: any, index: number): JSX.Element => {
          if (text === true) {
            return <span className='rule-type same'>相同</span>;
          } else {
            return <span className='rule-type no-same'>相似</span>;
          }
        }
      },
      {
        title: '版本号',
        dataIndex: 'version',
        width: '80px'
      },
      {
        title: '规则内容',
        dataIndex: 'ruleDetails',
        width: '220px',
        render: (text: any, record: any, index: number): JSX.Element => {
          let arr = [];
          for (let i = 0; i < this.ruleDetailList.length; i++) {
            for (let j = 0; j < record.ruleDetails.length; j++) {
              if (this.ruleDetailList[i].field === record.ruleDetails[j].field) {
                arr.push(this.ruleDetailList[i].fieldDes);
              }
            }
          }
          return <span>{arr.join('+')}</span>;
        }
      }, {
        title: '优先级',
        dataIndex: 'priority',
        width: '80px',
        render: (text: any, record: any, index: number): JSX.Element => {
          if (record.same) {
            return <span className='same-rule-priority'>{text}</span>;
          } else {
            return <span className='no-same-rule-priority'>{text}</span>;
          }
        }
      }
    ];
    return <Modal visible={this.historyFlag}
      className='history-modal'
      width='700px'
      title='历史'
      footer={null}
      onCancel={() => { this.historyFlag = false; }}>
      <div className='history-content'>
        <Table
          columns={columns}
          dataSource={toJS(this.historyRuleList)}
          // pagination={{ 'pageSize': 5, 'current': 1, 'total': 2 }}
          scroll={{ y: '100%' }}
          rowKey={(record: any) => { return record.id; }}
        />
      </div>
    </Modal>;
  }

  // select-change
  @action
  public selectChange = (value: any, props: any) => {
    this.updateRuleData[props] = value;
  }

  // input-change
  @action
  public inputChanage = (e: any, props: any) => {
    this.updateRuleData[props] = e.target.value;
  }

  // inputNumber-change
  @action
  public inputNunberChange = (value: any, props: any) => {
    const reg = /^[0-9]*[1-9][0-9]*$/;
    if (reg.test(value)) {
      this.updateRuleData[props] = value;
    } else {
      this.updateRuleData[props] = null;
    }
  }

  // 查看历史
  @action
  public lookHistory = (data: any) => {
    const id = data.id;
    ruleStore.getHistory(id).then(res => {
      if (res.length === 0) {
        message.warning('没有历史！');
      } else {
        this.historyRuleList = res;
        this.historyFlag = true;
      }
    });
  }

  // 点击新增
  @action
  public addRule = () => {
    runInAction(() => {
      this.addFlag = true;
      this.modalFalg = true;
      this.updateRuleData = {
        same: '相同',
        version: 1,
        ruleDetails: [],
        priority: null
      };
    });
  }

  // 点击编辑
  @action
  public editRule = (data: any) => {
    this.addFlag = false;
    this.modalFalg = true;
    let updateRuleData = JSON.parse(JSON.stringify(data));
    if (data.same) {
      updateRuleData.same = '相同';
    } else {
      updateRuleData.same = '相似';
    }
    updateRuleData.version = updateRuleData.latestVersion;
    updateRuleData.ruleDetails = [];
    data.ruleDetails.forEach(item => {
      updateRuleData.ruleDetails.push(item.field);
    });
    this.updateRuleData = updateRuleData;
  }

  // 新增取消
  @action
  public cancelEdit = () => {
    this.modalFalg = false;
  }

  // 新增确定
  @action public okEdit = () => {
    // 验证数据为空
    if (this.updateRuleData.ruleDetails.length === 0 || this.updateRuleData.priority === null) {
      message.warning('带*的为必填项！');
      return;
    }
    // 新增
    if (this.addFlag) {
      // 验证重复
      let arr = this.ruleList.filter(item => {
        const str = item.same ? '相同' : '相似';
        if (str === this.updateRuleData.same && item.priority === this.updateRuleData.priority) {
          return true;
        }
        return false;
      });
      if (arr.length > 0) {
        message.warning('同一规则类型的优先级不能重复！');
        return;
      }
      const updateRuleData = this.changData();
      updateRuleData.operateType = 1;
      ruleStore.saveRule(updateRuleData).then(res => {
        if (res) {
          ruleStore.getRules().then(res2 => {
            this.ruleList = res2;
            message.info('新增成功！');
            this.modalFalg = false;
          });
        }
      });
    } else {
      // 验证重复
      let arr = this.ruleList.filter(item => {
        const str = item.same ? '相同' : '相似';
        if (str === this.updateRuleData.same && item.priority === this.updateRuleData.priority
          && item.id !== this.updateRuleData.id) {
          return true;
        }
        return false;
      });
      if (arr.length > 0) {
        message.warning('同一规则类型的优先级不能重复！');
        return;
      }
      const updateRuleData = this.changData();
      updateRuleData.operateType = 2;
      ruleStore.saveRule(toJS(updateRuleData)).then(res => {
        if (res) {
          ruleStore.getRules().then(res2 => {
            this.ruleList = res2;
            message.info('修改成功！');
            this.modalFalg = false;
          });
        }
      });
    }
  }

  // 转为合法数据
  public changData(): any {
    // 转为合法数据
    let updateRuleData = JSON.parse(JSON.stringify(this.updateRuleData));
    if (this.updateRuleData.same === '相同') {
      updateRuleData.same = true;
    } else {
      updateRuleData.same = false;
    }
    updateRuleData.ruleDetails = [];
    let arr = [];
    for (let j = 0; j < this.updateRuleData.ruleDetails.length; j++) {
      updateRuleData.ruleDetails.push({ 'field': this.updateRuleData.ruleDetails[j] });
      for (let i = 0; i < this.ruleDetailList.length; i++) {
        if (this.ruleDetailList[i].field === this.updateRuleData.ruleDetails[j]) {
          arr.push(this.ruleDetailList[i].fieldDes);
        }
      }
    }
    updateRuleData.name = arr.join('+');
    return updateRuleData;
  }
  //#endregion
}
