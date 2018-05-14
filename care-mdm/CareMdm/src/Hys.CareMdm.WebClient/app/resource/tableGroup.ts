import httpProxy from '../common/http-proxy';

// 获取所有组信息
export async function getAllTableGroups(): Promise<any> {
    return httpProxy.get('/Group/groups');
}

// 获取子集组信息

export async function getTableGroupById(id: number): Promise<any> {
    return httpProxy.get('/Group/groups/' + id);
}

// 新增组信息
export async function addTableGroup(group: any): Promise<any> {
    return httpProxy.post('/Group/groups/' , group);
}

// 修改组信息
export async function updateTableGroup(group: any): Promise<any> {
    return httpProxy.put('/Group/groups/' , group);
}
