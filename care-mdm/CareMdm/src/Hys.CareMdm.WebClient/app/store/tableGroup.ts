import * as resource from '../resource/tableGroup';

export class TableGroupStore {
    public getAllTableGroups = () => {
        return resource.getAllTableGroups();
    }

    public getTableGroupById = (id: number) => {
        return resource.getTableGroupById(id);
    }

    public addTableGroup = (group: any) => {
        return resource.addTableGroup(group);
    }

    public updateTableGroup = (group: any) => {
        return resource.updateTableGroup(group);
    }
}
