export interface IMainData {
    /**
     * 表名
     */
    tableName: string;
}

/**
 * 表分组
 */
export interface ITableGroup {
    /**
     * 父节点id
     */
    patientId: number;

    /**
     * id
     */
    id: number;

    /**
     * 名字
     */
    name: string;
}

/**
 * 二级分组
 */

export interface ITableGroupItem {

    /**
     * id
     */
    id: number;

    /**
     * 描述
     */
    description: string;

    /**
     * 父节点id
     */
    parentId: number;

    /**
     * 名字
     */
    name: string;

    /**
     * 是否启用
     */
    isActive: boolean;
}
