export interface ITableData {
    data: any;
    columns: Array<ITableColumnData>;
    tableId: number;
    searchTable: any;
    total: number;
}

import { ITableColumnData } from '../table-column';
