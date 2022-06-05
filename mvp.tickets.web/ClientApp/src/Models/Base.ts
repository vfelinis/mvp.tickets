export enum ResponseCodes {
    Unknown = 0,
    Success = 200,
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    Error = 500,
}

export interface IBaseResponse {
    isSuccess: boolean,
    code: ResponseCodes,
    errorMessage: string,
}

export interface IBaseCommandResponse<T> extends IBaseResponse {
    data: T
}

export interface IBaseQueryResponse<T> extends IBaseResponse {
    data: T
}

export interface IBaseReportQueryResponse<T> extends IBaseQueryResponse<T>
{
    total: number
}