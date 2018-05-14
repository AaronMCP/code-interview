/**
 * 图表结构数据
 */

export interface ITableColumn {

    /**
     * 表
     */
    tableColumn: Array<ITableColumnData>;
    tableId: number;
    columnSure: any;
}

/**
 * 数据结构
 */
export interface ITableColumnData {

    editable?: boolean;
    /**
     * id
     */
    id?: any;

    /**
     * 列名
     */
    name: string;

    /**
     * 列名code
     */
    code: string;

    /**
     * 描述
     */
    description: string;

    /**
     * 数据类型
     */
    dataType: string;

    /**
     * 是否允许null值
     */
    nullable: boolean;

    /**
     * 是否可搜索
     */

    searchable: boolean;

    /**
     * 是否默认搜索条件
     */

    isDefaultSearch: boolean;

    /**
     * 是否为主键
     */

    isPrimary: boolean;
}
