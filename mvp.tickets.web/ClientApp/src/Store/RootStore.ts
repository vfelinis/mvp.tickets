import { createContext, useContext } from 'react';
import { ErrorStore } from './ErrorStore';
import { UserStore } from './UserStore';

export class RootStore {
  userStore: UserStore;
  errorStore: ErrorStore;

  constructor() {
    this.userStore = new UserStore(this);
    this.errorStore = new ErrorStore(this);
  }
}

const RootStoreContext = createContext(new RootStore());

export function useRootStore() {
  return useContext(RootStoreContext)
}
