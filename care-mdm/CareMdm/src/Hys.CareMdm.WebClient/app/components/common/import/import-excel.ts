import * as XLSX from 'xlsx';

// 创建FileReader对象并读取文件信息，读取完成后会把读取的 文件数据 传入到回调中
export const readFile = (file: any, cb: (e: any) => void) => {
  const reader = new FileReader();
  reader.onload = (event: any) => {
    const data = event.target.result;
    cb(data);
  };
  reader.readAsArrayBuffer(file);
};

// 读取文件数据为 workbook, 这是excel相关的所有信息
export const readExcel = (data: any) => {
  data = new Uint8Array(data);
  const workbook = XLSX.read(data, { type: 'array' });
  // tslint:disable-next-line:no-console
  // console.log(workbook);
  return workbook;
};

// 把excel中第一个表整理成dom
export const getDOM = (workbook: any) => {
  const worksheet = workbook.Sheets[workbook.SheetNames[0]];
  const dom = XLSX.utils.sheet_to_html(worksheet);
  // tslint:disable-next-line:no-console
  // console.log(dom);
  return dom;
};

// 把excel中第一个表整理成json数组
export const getJSON = (workbook: any) => {
  const worksheet = workbook.Sheets[workbook.SheetNames[0]]; // 获取excel中的第一个sheet
  const jsonA = XLSX.utils.sheet_to_json(worksheet, { header: 'A' }); // 以键值对的方式生成, 列名就是健，比如excel中的 A,B,C
  // const json1 = XLSX.utils.sheet_to_json(worksheet, { header: 1 }); // 以值的方式生成, 不包含列名
  // tslint:disable-next-line:no-console
  // console.log(jsonA);
  // tslint:disable-next-line:no-console
  // console.log(json1);
  return jsonA;
};
const s2ab = (s: any) => {
  if (typeof ArrayBuffer !== 'undefined') {
    const buf = new ArrayBuffer(s.length);
    const view = new Uint8Array(buf);
    for (let i = 0; i !== s.length; ++i) {
      // tslint:disable-next-line:no-bitwise
      view[i] = s.charCodeAt(i) & 0xff;
    }
    return buf;
  } else {
    const buf = new Array(s.length);
    for (let i = 0; i !== s.length; ++i) {
      // tslint:disable-next-line:no-bitwise
      buf[i] = s.charCodeAt(i) & 0xff;
    }
    return buf;
  }
};

const saveAs = (blob: any, filename: any) => {
  const type = blob.type;
  const savetype = 'application/octet-stream';
  if (type && type !== savetype) {
    // 强制下载，而非在浏览器中打开
    const slice = blob.slice || blob.webkitSlice || blob.mozSlice;
    blob = slice.call(blob, 0, blob.size, savetype);
  }

  const url = URL.createObjectURL(blob);
  const savelink: any = document.createElement('a');
  savelink.href = url;
  savelink.download = filename;

  const event = document.createEvent('MouseEvents');
  event.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
  savelink.dispatchEvent(event);
  URL.revokeObjectURL(url);
};
