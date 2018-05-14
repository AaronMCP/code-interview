export interface IEditcell {
  editable: boolean;
  value: string;
  onChange: Function;
  status: string;
  fields?: Array<any>;
}
