export interface IEditcell {
  editable: boolean;
  value: any;
  onChange: Function;
  dataType?: string;
  selectAble?: boolean;
  selectList?: Array<string>;
}
