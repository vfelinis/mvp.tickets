import axios from 'axios';
import { observable, action, makeObservable } from 'mobx';
import { IBaseReportQueryResponse, IBaseCommandResponse } from '../Models/Base';
import { RootStore } from './RootStore';
import { ApiRoutesHelper } from '../Helpers/ApiRoutesHelper';
import { Permissions } from '../Models/Permissions';
import { IUserModel } from '../Models/User';

export class UserStore {
    private rootStore: RootStore;
    user: IUserModel | null;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
        this.user = null;
        makeObservable(this, {
            user: observable,
            login: action,
            logout: action,
            setUser: action
        });
    }

    login(idToken: string): void {
        axios.post<IBaseCommandResponse<IUserModel>>(ApiRoutesHelper.user.login, { idToken: idToken })
            .then(response => {
                if (response.data.isSuccess) {
                    this.setUser(response.data.data);

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
                    this.setUser(null);

                } else {
                    this.rootStore.errorStore.setError(response.data.errorMessage ?? response.data.code.toString());
                }
            })
            .catch(error => {
                this.rootStore.errorStore.setError(JSON.stringify(error));
            })
    }

    setUser(user: IUserModel | null): void {
        this.user = user;
    }
}