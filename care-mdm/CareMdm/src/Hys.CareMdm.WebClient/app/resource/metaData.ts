import httpProxy from '../common/http-proxy';

// 获取所有接口信息
export async function getAllInterfaces(): Promise<any> {
    return httpProxy.get('/OutInterface/outinterface/');
}

// 根据接口id获取接口信息
export async function getOutInterfaceById(id: number): Promise<any> {
    return httpProxy.get('/OutInterface/outinterface/' + id);
}

// 新增接口信息
export async function addOutInterface(outInterface: any): Promise<any> {
    return httpProxy.post('/OutInterface/outinterface/', outInterface);
}

// 修改接口信息
export async function updateOutInterface(outInterface: any): Promise<any> {
    return httpProxy.put('/OutInterface/outinterface/', outInterface);
}

// 根据接口id获取参数信息
export async function getParameterById(id: number): Promise<any> {
    return httpProxy.get('/OutInterface/outparameter/' + id);
}

// 新增参数
export async function addOutParameter(parameter: any): Promise<any> {
    return httpProxy.post('/OutInterface/outparameter/', parameter);
}

// 修改参数
export async function updateOutParameter(parameter: any): Promise<any> {
    return httpProxy.put('/OutInterface/outparameter/', parameter);
}
