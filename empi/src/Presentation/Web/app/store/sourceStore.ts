import * as source from '../resource/sourceResource';

export class SourceStore {

  public getSources = () => {
    return source.getSources();
  }

  public saveSource = (sourceParameter: any) => {
    return source.saveSource(sourceParameter);
  }

  public getSourceHistory = () => {
    return source.getSourceHistory();
  }
}
