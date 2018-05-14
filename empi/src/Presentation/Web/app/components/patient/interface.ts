export interface IPatient {
  addPatient: Function;
  getSources: Function;
}

export interface IPatientData {
  id: number;
  empid: number;
  name: string;
  sex: string;
  birthday: Date;
  credentialType: string;
  credentialNumber: string;
  medicalInsuranceNo: string;
  socialSecurityNo: string;
  phone: string;
  address: string;
}

export interface IRawPatient {
  sourceId: number;
  sourceVersion: number;
  sourcePatientId: string;
  name: string;
  sex: string;
  birthday: Date;
  credentialType: string;
  credentialNumber: string;
  bornProvince: string;
  bornContry: string;
  medicalInsuranceNo: string;
  socialSecurityNo: string;
  maritalStatus: string;
  nationality: string;
  phone: string;
  address: string;
  createdAt: Date;
  sourceCreatedAt: Date;
}
