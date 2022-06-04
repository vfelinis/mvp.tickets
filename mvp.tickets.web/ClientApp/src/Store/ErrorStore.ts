import { observable, action, makeObservable } from 'mobx';
import { RootStore } from './RootStore';

export class ErrorStore {
    private rootStore: RootStore;
    errors: string[];

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
        this.errors = [];
        makeObservable(this, {
            errors: observable,
            setError: action,
        });
    }

    setError(error: string): void {
        this.errors = [...this.errors, error];
    }

    clearErrors(): void {
        this.errors = [];
    }
}