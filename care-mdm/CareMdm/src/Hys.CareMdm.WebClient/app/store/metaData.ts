import * as resource from '../resource/metaData';

export class MetaDataStore {
    public getAllInterfaces = () => {
        return resource.getAllInterfaces();
    }

    public getOutInterfaceById = (id: number) => {
        return resource.getOutInterfaceById(id);
    }

    public addOutInterface = (outInterface: any) => {
        return resource.addOutInterface(outInterface);
    }

    public updateOutInterface = (outInterface: any) => {
        return resource.updateOutInterface(outInterface);
    }

    public getParameterById = (id: number) => {
        return resource.getParameterById(id);
    }

    public addOutParameter = (parameter: any) => {
        return resource.addOutParameter(parameter);
    }
    public updateOutParameter = (parameter: any) => {
        return resource.updateOutParameter(parameter);
    }
}
