import axios from 'axios';
import { observable, action, makeObservable } from 'mobx';
import { IBaseReportQueryResponse, IBaseCommandResponse } from '../Models/Base';
import { RootStore } from './RootStore';
import { ApiRoutesHelper } from '../Helpers/ApiRoutesHelper';

export class UserStore {
    private rootStore: RootStore;
    usersReport: IUserReportModel[];
    user: IUserModel | null;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
        this.usersReport = [];
        this.user = null;
        makeObservable(this, {
            usersReport: observable,
            user: observable,
            getUsersReport: action,
        });
    }

    login(idToken: string): void {
        axios.post<IBaseCommandResponse<IUserModel>>(ApiRoutesHelper.user.login, { idToken: idToken })
            .then(response => {
                if (response.data.isSuccess) {
                    this.user = response.data.data;

                } else {
                    this.rootStore.errorStore.setError(response.data.errorMessage ?? response.data.code.toString());
                }
            })
            .catch(error => {
                this.rootStore.errorStore.setError(JSON.stringify(error));
            })
    }

    logout(): void {
        axios.post<IBaseCommandResponse<object>>(ApiRoutesHelper.user.logout)
            .then(response => {
                if (response.data.isSuccess) {
                    this.user = null;

                } else {
                    this.rootStore.errorStore.setError(response.data.errorMessage ?? response.data.code.toString());
                }
            })
            .catch(error => {
                this.rootStore.errorStore.setError(JSON.stringify(error));
            })
    }

    setUser(user: IUserModel): void {
        this.user = user;
    }

    getUsersReport(): void {
        axios.get<IBaseReportQueryResponse<IUserReportModel[]>>('/api/users/report')
            .then(response => {
                this.usersReport = response.data.data
            })
            .catch(error => {
                alert(error)
            })
    }
}

export interface IUserModel {
    id: number
    email: string
    firstName: string
    lastName: string
    permissions: number
    isLocked: boolean
}

export interface IUserReportModel extends IUserModel {
    dateCreated: Date
    dateModified: Date
}
