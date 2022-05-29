export enum ResponseCodes {
    Unknown = 0,
    Success = 200,
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    Error = 500,
}

export interface IBaseResponse {
    isSuccess: boolean
    code: ResponseCodes
    errorMessage: string
}

export interface IBaseCommandResponse extends IBaseResponse {}

export interface IBaseQueryResponse extends IBaseResponse {}

export interface IBaseReportQueryResponse<T> extends IBaseQueryResponse
{
    data: T[]
    total: number
}