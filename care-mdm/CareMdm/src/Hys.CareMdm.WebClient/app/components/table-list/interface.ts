/**
 * menu结构
 */
export interface ITableList {

    /**
     * 新增内容所属分组
     */
    groupText: string;

    /**
     * 弹出框数据
     */
    tableListData: ITableListData;

    /**
     * 确定函数
     */
    onOk: any;

    /**
     * 取消函数
     */
    onCancel: any;

    /**
     * 弹出flag
     */
    modalListFlag: boolean;

    /**
     * 新增flag
     */
    addListFlag: boolean;

    /**
     * 输入框变化函数
     */
    inputChange: any;

    /**
     * 日期变化函数
     */
    dateChange: any;

    checkChange: any;
}

/**
 * 弹出框数据结构
 */

export interface ITableListData {

    id: number;
    groupId: number;

    /**
     * 标准代码
     */
    standardCode: string;

    /**
     * 中文名称
     */
    standardCnName: string;

    /**
     * 中文简称
     */
    name: string;

    /**
     * 英文名称
     */
    standardEnName: string;

    /**
     * 英文简称
     */
    code: string;

    /**
     * 是否启用
     */
    isActive: boolean;

    /**
     * 启用时间
     */
    activeTime: Date;

    /**
     * 停用时间
     */
    deactiveTime: Date;

    /**
     * 数据描述
     */
    description: string;

    /**
     * 备注
     */
    comments: string;
}
