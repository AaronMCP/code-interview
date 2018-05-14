/**
 * 数据源
 */
export interface ISourceData {

  /**
   * 主键
   */
  id?: number;

  /**
   * 最新版本
   */
  latestVersion: number;

  /**
   * 数据源名称
   */
  name: string;

  /**
   * 优先级
   */
  priority: number;
}

/**
 * 数据源历史
 */
export interface ISourceHistoryData {
  /**
   * 主键
   */
  id: number;

  /**
   * 最新版本
   */
  version: number;

  /**
   * 数据源名称
   */
  name: string;

  /**
   * 优先级
   */
  priority: number;
}
