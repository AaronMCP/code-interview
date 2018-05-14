import httpProxy from '../common/http-proxy';

// 获取所有的rules
export async function getRules(): Promise<any> {
  return httpProxy.get('/rule');
}

// 根据id获取ruleDetail
export async function getRuleDetail(id: number): Promise<any> {
  return httpProxy.get('/rule/' + id);
}

export async function saveRule(rule: any): Promise<any> {
  return httpProxy.post('/rule/saverule', rule);
}

// export async function addRule(rule: any): Promise<any> {
//   console.log(rule);
//   return httpProxy.post('/rule', rule);
// }

// export async function updateRule(rule: any): Promise<any> {
//   return httpProxy.put('/rule', {'rule': rule});
// }

export async function getFields(): Promise<any> {
  return httpProxy.get('/rule/patientcols');
}

export async function getHistory(id: number): Promise<any> {
  return httpProxy.get('/rule/history/' + id);
}
