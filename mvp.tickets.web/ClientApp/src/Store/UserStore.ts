import axios from "axios"
import { observable, action, makeObservable } from 'mobx';
import { IBaseReportQueryResponse } from "../Models/Base";
import { RootStore } from "./RootStore";

export class UserStore {
    private rootStore: RootStore;
    usersReport: IUserReportModel[]

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
        this.usersReport = []
        makeObservable(this, {
            usersReport: observable,
            getUsersReport: action,
        })
    }

    getUsersReport(): void {
        axios.get<IBaseReportQueryResponse<IUserReportModel>>('/api/users/report')
            .then(response => {
                this.usersReport = response.data.data
            })
            .catch(error => {
                alert(error)
            })
    }
}

export interface IUserReportModel {
    id: number
    email: string
    firstName: string
    lastName: string
    permissions: number
    isActive: boolean
    isLocked: boolean
    dateCreated: Date
    dateModified: Date
}
