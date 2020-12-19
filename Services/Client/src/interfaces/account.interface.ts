import { TransactionInterface } from './transaction.interface';

// General
export interface AccountInterface {
  uuid?: string;
  profile: string;
  status: string;
  balance: number;
  transactions?: TransactionInterface[]
}

// Account Detail View
export interface AccountReducerState {
  loading: boolean,
  error: boolean,
  data?: AccountInterface
}
export interface AccountAction {
  type: string,
  params?: any
  state: AccountReducerState,
}

// Accounts Overview
export interface AccountsOverviewReducerState {
  loading: boolean,
  error: boolean,
  data?: AccountInterface[]
}
export interface AccountsOverviewAction {
  type: string,
  params?: any
  state: AccountsOverviewReducerState,
}
