import * as React from 'react';
import { Patient } from './presenter';
import { observer } from 'mobx-react';
import { PatientStore, SourceStore } from '../../store';

const patientStore = new PatientStore();
const sourceStore = new SourceStore();
@observer
export class PatientContainer extends React.Component<{}, {}> {
  constructor(props: {}) {
    super(props);
  }

  public render(): JSX.Element {
    return <Patient
      addPatient={patientStore.addPatient}
      getSources={sourceStore.getSources}
    />;
  }
}
