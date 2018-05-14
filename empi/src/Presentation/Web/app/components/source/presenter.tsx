import * as React from 'react';
import { ISourceData, ISourceHistoryData } from './interface';
import { Button, Table, Modal, Select, Form, Input, message, InputNumber } from 'antd';
import { SourceStore } from '../../store';
import { observer } from 'mobx-react';
import { observable, action, toJS, runInAction } from 'mobx';
import './style.scss';

const sourceStore = new SourceStore();
const FormItem = Form.Item;
const confirm = Modal.confirm;
const formLayout = {
  labelCol: { span: 6 },
  wrapperCol: { span: 16 }
};
const Option = Select.Option;

@observer
export class Source extends React.Component<{}, {}> {
  @observable public addFlag: boolean;
  @observable public modalFalg: boolean;
  @observable public historyFlag: boolean;
  @observable public updateSourceData: any;
  @observable public historyEdition: any;
  @observable public sourceList: Array<ISourceData>;
  @observable public sourceHistoryList: Array<ISourceHistoryData>;
  constructor(props: {}) {
    super(props);
    this.addFlag = false;
    this.modalFalg = false;
    this.historyFlag = false;
    this.historyEdition = {
      versions: [],
      edition: '',
      value: ''
    };
    this.sourceList = [];
    this.sourceHistoryList = [];
    this.updateSourceData = {
      'version': null,
      'name': '',
      'priority': null
    };
  }

  // 初始化请求数据
  @action
  public componentDidMount(): void {
    // 请求数据源list
    sourceStore.getSources().then(res => {
      this.sourceList = res;
    });
  }

  public render(): JSX.Element {
    // 表格column
    const columns = [
      {
        title: '数据源名称',
        dataIndex: 'name',
        width: '20%'
      }, {
        title: '优先级',
        dataIndex: 'priority',
        width: '20%',
        render: (text: any, record: any, index: number): JSX.Element => {
          return <span className='source-priority'>{text}</span>;
        }
      }, {
        title: '操作',
        dataIndex: 'operate',
        width: '40%',
        render: (text: any, record: any, index: number): JSX.Element => {
          return <div className='source-table-operate'>
            <span className='source-edit' onClick={() => this.editSource(record)}>编辑</span>
          </div>;
        }
      }
    ];
    let box = document.querySelector('.source-content');
    const y = box ? (box.clientHeight - 42) : 440;
    console.log(y);
    return (
      <div className='source'>
        <div className='operate-container'>
          <Button onClick={this.addSource}>新增</Button>
          <Button onClick={this.lookHistory}>历史</Button>
        </div>
        <div className='source-content'>
          <Table bordered
            scroll={{y}}
            rowKey={(record: any) => { return record.id; }}
            pagination={false}
            columns={columns}
            dataSource={toJS(this.sourceList)}
          />
          {this.getSourceModal()}
          {this.getHistoryModal()}
        </div>
      </div>);
  }

  //#region 新增、修改数据源、查看历史
  // 新增数据源modal
  public getSourceModal = () => {
    return <Modal
      width='376px'
      title={this.addFlag ? '新增数据源' : '修改数据源'}
      okText={this.addFlag ? '确定添加' : '确定保存'}
      cancelText='取消'
      visible={this.modalFalg}
      onOk={this.okEdit}
      onCancel={this.cancelEdit}
    >
      <Form>
        <FormItem {...formLayout} label='版本号'>
          <InputNumber value={this.updateSourceData.version ? this.updateSourceData.version : ''}
            disabled />
        </FormItem>
        <FormItem {...formLayout} label='* 数据源名称' className='form-required'>
          <Input value={this.updateSourceData.name ? this.updateSourceData.name : ''}
            onChange={(e) => this.inputChange(e, 'name')} />
        </FormItem>
        <FormItem {...formLayout} label='* 优先级' className='form-required'>
          <InputNumber min={1} value={this.updateSourceData.priority ? this.updateSourceData.priority : ''}
            onChange={(value) => this.inputNumberChange(value, 'priority')} />
        </FormItem>
      </Form>
    </Modal>;
  }

  // 历史modal
  public getHistoryModal = () => {
    const columns = [{
      title: '版本号',
      dataIndex: 'sourceVersion',
      width: '33.3%'
    }, {
      title: '数据源名称',
      dataIndex: 'name',
      width: '33.3%'
    }, {
      title: '优先级',
      dataIndex: 'priority',
      width: '33.3%'
    }];
    const y = 280;
    return <Modal visible={this.historyFlag}
      className='history-modal'
      width='376px'
      title='历史'
      footer={<Button key='ok' onClick={() => { this.historyFlag = false; }}>确定</Button>}
      onCancel={() => { this.historyFlag = false; }}>
      <div className='history-select'>
        <Select defaultValue={`${this.historyEdition.edition}`} style={{ width: 159 }} onChange={this.historyChange}>
          <Option value='0'>全部版本</Option>
          {
            this.historyEdition.versions.map ((item, key) =>
             <Option key={key} value={`${item.sourceHistoryId}`}>{item.sourceHistoryId}</Option>
            )
          }
        </Select>
      </div>
      <div className='history-content'>
        <Table
          columns={columns}
          dataSource={toJS(this.sourceHistoryList)}
          scroll={{y: y}}
          pagination={false}
          rowKey={(record: any) => { return record.id; }}
        />
      </div>
    </Modal>;
  }

  // input-change
  @action
  public inputChange = (e: any, props: any) => {
    this.updateSourceData[props] = e.target.value;
  }

  // input-number-change
  @action
  public inputNumberChange = (value: any, props: any) => {
    this.updateSourceData[props] = value;
  }

  // 历史版本筛选
  public historyChange = (value: any) => {
    sourceStore.getSourceHistory().then(res => {
      const temp = [];
      res.map (item =>
        item.sourceHistoryId === (value - 0 ) ? temp.push(item) : ''
      );
      value === '0' ? this.sourceHistoryList = res : this.sourceHistoryList = temp;
    });
    this.historyEdition.value = value;
  }

  // 查看历史
  @action
  public lookHistory = () => {
    if (this.sourceList.length === 0) {
      message.warning('请先添加数据源！');
      return;
    }
    sourceStore.getSourceHistory().then(res => {
      const temp = {};
      this.historyEdition.versions = [];
      res.reduce((item, next) => {
          return temp[next.sourceHistoryId] ?
           '' : temp[next.sourceHistoryId] = true && this.historyEdition.versions.push(next);
      });
      const value = this.historyEdition.value ? Number(this.historyEdition.value) : temp['1'];
      this.historyEdition.edition = value;
      this.historyChange(value);
      if (res.length > 0) {
        this.historyFlag = true;
        this.sourceHistoryList = res;
      } else {
        message.warning('没有历史！');
      }
    });
  }

  // 点击新增
  @action
  public addSource = () => {
    this.addFlag = true;
    this.modalFalg = true;
    this.updateSourceData = {
      'name': '',
      'version': this.sourceList.length > 0 ? this.sourceList[0].latestVersion : 1,
      'priority': null
    };
  }

  // 点击编辑
  @action
  public editSource = (data: any) => {
    this.addFlag = false;
    this.modalFalg = true;
    this.updateSourceData = JSON.parse(JSON.stringify(data));
    console.log(data);
  }

  // 新增取消
  @action
  public cancelEdit = () => {
    this.modalFalg = false;
  }

  // 新增确定
  @action
  public okEdit = () => {
    // 验证必填项
    if (this.updateSourceData.name.replace(/(^\s*)|(\s*$)/g, '') === '' || this.updateSourceData.priority === null) {
      message.warning('带*的为必填项！');
      return;
    }
    if (this.addFlag) {
      // 新增数据源
      if (this.checkData(true)) {
        sourceStore.saveSource([toJS(this.updateSourceData)]).then(res => {
          if (res) {
            sourceStore.getSources().then(res2 => {
              this.sourceList = res2;
              message.info('新增成功！');
              this.modalFalg = false;
            });
          }
        });
      }
    } else {
      // 修改数据源
      if (this.checkData(false)) {
        sourceStore.saveSource([toJS(this.updateSourceData)]).then(res => {
          if (res) {
            sourceStore.getSources().then(res2 => {
              this.sourceList = res2;
              message.info('修改成功！');
              this.modalFalg = false;
            });
          }
        });
      }
    }
  }

  // 验证数据不重复
  public checkData(addFlag: boolean): boolean {
    if (addFlag) {
      // 验证数据源不重复
      let arrName = this.sourceList.filter(item => item.name === this.updateSourceData.name);
      if (arrName.length > 0) {
        message.warning('数据源名称不能不能重复！');
        return false;
      }

      // 验证优先级不重复
      let arrPriority = this.sourceList.filter(item => item.priority === this.updateSourceData.priority);
      if (arrPriority.length > 0) {
        message.warning('优先级不能不能重复！');
        return false;
      }
    } else {
      // 验证数据源不重复
      let arrName = this.sourceList.filter(item => item.name === this.updateSourceData.name &&
        item.id !== this.updateSourceData.id);
      if (arrName.length > 0) {
        message.warning('数据源名称不能不能重复！');
        return false;
      }

      // 验证优先级不重复
      let arrPriority = this.sourceList.filter(item => item.priority === this.updateSourceData.priority &&
        item.id !== this.updateSourceData.id);
      if (arrPriority.length > 1) {
        message.warning('优先级不能不能重复！');
        return false;
      }
    }

    return true;
  }
  //#endregion
}
