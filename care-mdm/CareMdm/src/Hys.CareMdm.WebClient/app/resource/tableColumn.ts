import httpProxy from '../common/http-proxy';

// 根据id获取columns
export async function getColums(id: number): Promise<any> {
    return httpProxy.get('/Column/columns/' + id);
}

// 新增colums
export async function addColums(id: number, column: any): Promise<any> {
    return httpProxy.post('/Column/columns/' + id, column);
}

// 修改colums
export async function updateColums(id: number, column: any): Promise<any> {
    return httpProxy.put('/Column/columns/' + id, column);
}
