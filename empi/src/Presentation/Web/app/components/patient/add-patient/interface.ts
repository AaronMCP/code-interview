// import { TableData } from '../../source';
import { IRawPatient } from '../interface';
export interface IAddPatient {
  sources: Array<any>;
  showFlag: boolean;
  cancel: any;
  ok: any;
  rawPatient: IRawPatient;
  dateChange: any;
  sexChange: any;
  sourceChange: any;
  inputChange: any;
  birthday: any;
}
