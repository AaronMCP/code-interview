import * as resource from '../resource/tableColumn';

export class TableColumnStore {
    public getColums = (id: number) => {
        return resource.getColums(id);
    }
    public addColums = (id: number, table: any) => {
        return resource.addColums(id, table);
    }
    public updateColums = (id: number, table: any) => {
        return resource.updateColums(id, table);
    }
}
