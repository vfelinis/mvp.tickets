import { createContext, useContext } from 'react';
import { CategoryStore } from './CategoryStore';
import { ErrorStore } from './ErrorStore';
import { UserStore } from './UserStore';

export class RootStore {
  userStore: UserStore;
  errorStore: ErrorStore;
  categoryStore: CategoryStore;

  constructor() {
    this.userStore = new UserStore(this);
    this.errorStore = new ErrorStore(this);
    this.categoryStore = new CategoryStore(this);
  }
}

const RootStoreContext = createContext(new RootStore());

export function useRootStore() {
  return useContext(RootStoreContext)
}
