import httpProxy from '../common/http-proxy';

// 获取所有数据描述
export async function getAllTable(): Promise<any> {
    return httpProxy.get('/Table/tables');
}

// 根据id获取一组数据描述
export async function getTableByGroup(id: number): Promise<any> {
    return httpProxy.get('/Table/tablesbygroup/' + id);
}

// 根据id获取数据描述
export async function getTableById(id: number): Promise<any> {
    return httpProxy.get('/Table/tablebyid/' + id);
}

// 新增数据描述
export async function addTableInfo(table: any): Promise<any> {
    return httpProxy.post('/Table/tables', table);
}

// 修改数据描述
export async function upateTableInfo(table: any): Promise<any> {
    return httpProxy.put('/Table/tables', table);
}
