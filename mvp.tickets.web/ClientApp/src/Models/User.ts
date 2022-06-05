import { Permissions } from './Permissions';

export interface IUserModel {
    id: number,
    email: string,
    firstName: string,
    lastName: string,
    permissions: Permissions,
    isLocked: boolean,
    dateCreated: Date,
    dateModified: Date,
}