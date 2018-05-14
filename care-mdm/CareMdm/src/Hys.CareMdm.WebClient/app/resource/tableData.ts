import httpProxy from '../common/http-proxy';

// 获取所有图表数据
export async function getTableData(id: number): Promise<any> {
    return httpProxy.get('/TableData/tabledata/' + id);
}

// 搜索所有图表数据
export async function searchTableData(id: number, pageSize: number, pageIndex: number, json: string): Promise<any> {
    return httpProxy.get('/TableData/tabledata/' + json + '/' + id + '/' + pageSize + '/' + pageIndex);
}

// 新增图表数据
export async function addTableData(id: number, tableData: any): Promise<any> {
    return httpProxy.post('/TableData/tabledata/' + id, tableData);
}

// 修改图表数据
export async function updateTableData(id: number, tableData: any): Promise<any> {
    return httpProxy.put('/TableData/tabledata/' + id, tableData);
}

// 启用或禁用数据

export async function activeTableData(id: number, tableData: any): Promise<any> {
    return httpProxy.put('/TableData/activetabledata/' + id, tableData);
}
// 导入
export async function importTableData(id: number, tableData: any): Promise<any> {
    return httpProxy.post('/TableData/importtabledata/' + id, tableData);
}
