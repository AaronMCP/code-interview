import * as resource from '../resource/tableData';

export class TableDataStore {
    public getTableData = (id: number) => {
        return resource.getTableData(id);
    }

    public searchTableData = (id: number, pageSize: number, pageIndex: number, json: any) => {
        return resource.searchTableData(id, pageSize, pageIndex, json);
    }

    public addTableData = (id: number, tableData: any) => {
        return resource.addTableData(id, tableData);
    }

    public updateTableData = (id: number, tableData: any) => {
        return resource.updateTableData(id, tableData);
    }

    public activeTableData = (id: number, tableData: any) => {
        return resource.activeTableData(id, tableData);
    }
    public importTableData = (id: number, tableData: any) => {
        return resource.importTableData(id, tableData);
    }
}
