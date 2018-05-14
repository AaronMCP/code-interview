import * as resource from '../resource/tableDescription';

export class TableDescriptionStore {
    public getAllTable = () => {
        return resource.getAllTable();
    }
    public getTableByGroup = (id: number) => {
        return resource.getTableByGroup(id);
    }
    public getTableById = (id: number) => {
        return resource.getTableById(id);
    }
    public addTableInfo = (table: any) => {
        return resource.addTableInfo(table);
    }
    public upateTableInfo = (table: any) => {
        return resource.upateTableInfo(table);
    }
}
