import { createContext, useContext } from 'react';
import { UserStore } from './UserStore';

export class RootStore {
  userStore: UserStore;

  constructor() {
    this.userStore = new UserStore(this);
  }
}

const RootStoreContext = createContext(new RootStore());

export function useRootStore() {
  return useContext(RootStoreContext)
}
