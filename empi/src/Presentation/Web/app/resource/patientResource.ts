import httpProxy from '../common/http-proxy';
import { IPatientData } from '../components/patient';

// 获取病人
export async function getPatients(patientCriteria: any): Promise<any> {
  return httpProxy.post('/patient/getPatients', patientCriteria);
}

// 病人合并
export async function mergePatients(mergePatient: any): Promise<any> {
  return httpProxy.put('/patient/merge', mergePatient);
}

// 相似病人标记为新病人
export async function distinctPatient(patientId: number): Promise<any> {
  return httpProxy.put('/patient/distinct/' + patientId);
}

// 根据病人id获取相似病人
export async function getSimilarPatient(patientId: number): Promise<any> {
  return httpProxy.put('/patient/getsimilar/' + patientId);
}

// 根据病人empiId获取病人历史记录
export async function getHistory(empiId: string): Promise<any> {
  return httpProxy.get('/patient/gethistory/' + empiId);
}

// 根据病人id获取源数据
export async function getRawPatient(id: number): Promise<any> {
  return httpProxy.get('/patient/getrawpatient/' + id);
}

// 拆分病人
export async function splitPatient(patientId: number, rawPatient: any): Promise<any> {
  return httpProxy.put('/patient/split/' + patientId, rawPatient);
}
