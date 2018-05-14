import { observable, action, runInAction } from 'mobx';
import { IPatientData } from '../components/patient';
import * as resouce from '../resource/patientResource';

export class PatientStore {

  public getPatients = (patientCriteria: any) => {
    return resouce.getPatients(patientCriteria);
  }

  public addPatient = (patientCriteria: any) => {
    return resouce.getPatients(patientCriteria);
  }

  public mergePatients = (mergePatient: any) => {
    return resouce.mergePatients(mergePatient);
  }

  public distinctPatient = (patientId: number) => {
    return resouce.distinctPatient(patientId);
  }

  public getSimilarPatient = (patientId: number) => {
    return resouce.getSimilarPatient(patientId);
  }

  public getHistory = (empiId: string) => {
    return resouce.getHistory(empiId);
  }

  public getRawPatient = (id: number) => {
    return resouce.getRawPatient(id);
  }

  public splitPatient = (patientId: number, rawPatient: any) => {
    return resouce.splitPatient(patientId, rawPatient);
  }
}
