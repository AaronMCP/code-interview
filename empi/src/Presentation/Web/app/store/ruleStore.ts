import * as resource from '../resource/ruleResource';

export class RuleStore {

  // 获取所有规则
  public getRules = () => {
    return resource.getRules();
  }

  // 根据id获取规则详细内容
  public getRuleDetail = (id: number) => {
    return resource.getRuleDetail(id);
  }

  //
  public saveRule = (rule: any) => {
    return resource.saveRule(rule);
  }

  // 新增规则
  // public addRule = (rule: any) => {
  //   return resource.addRule(rule);
  // }

  // // 修改规则
  // public updateRule = (rule: any) => {
  //   return resource.updateRule(rule);
  // }

  // 获取可选择的规则内容
  public getFields = () => {
    return resource.getFields();
  }

  // 获取规则历史内容
  public getHistory = (id: number) => {
    return resource.getHistory(id);
  }
}
