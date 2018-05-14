import httpProxy from '../common/http-proxy';

export async function getSources(): Promise<any> {
  return httpProxy.get('/sources');
}

export async function saveSource(source: any): Promise<any> {
  return httpProxy.post('/sources', source);
}

export async function getSourceHistory(): Promise<any> {
  return httpProxy.get('/sources/history');
}
