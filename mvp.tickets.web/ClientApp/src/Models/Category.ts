export interface ICategoryModel {
    id: number,
    name: string,
    isRoot: boolean,
    isActive: boolean,
    parentCategoryId: number | null,
    parentCategory: string,
    dateCreated: Date,
    dateModified: Date,
}

export interface ICategoryQueryRequest {
    id?: number | null,
    onlyActive: boolean,
    onlyRoot: boolean,
}

export interface ICategoryCreateCommandRequest {
    name: string,
    isActive: boolean,
    parentCategoryId: number | null
}

export interface ICategoryUpdateCommandRequest extends ICategoryCreateCommandRequest {
    id: number,
}