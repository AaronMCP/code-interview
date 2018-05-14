/**
 * 规则
 */
export interface IRuleData {

  /**
   * 主键
   */
  id?: number;

  /**
   * 是否相同规则 true表示相同规则 false表示相似规则
   */
  same: boolean;

  /**
   * 最新版本
   */
  latestVersion?: number;

  /**
   * 规则内容
   */
  ruleContent: Array<string>;

  /**
   * 优先级
   */
  priority: number;
}

/**
 * 可选的规则内容集合
 */
export interface IRuleDetailData {

  /**
   * 英文
   */
  field: string;

  /**
   * 中文
   */
  fieldDes: string;
}
