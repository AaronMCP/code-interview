export interface IMetaData {
    outInterface: IOutInterface;
    paramters: Array<IParamter>;
}

export interface IOutInterface {
    id: number;
    routeName: string;
    description: string;
}

export interface IParamter {
    editable?: boolean;
    id?: any;
    outInterfaceId: number;
    paramName: string;
    paramValue: string;
    description: string;
}
