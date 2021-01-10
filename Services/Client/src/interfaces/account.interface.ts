import {TransactionInterface} from './transaction.interface';

// General
export interface AccountInterface {
    id: string
    accountId?: string
    version: number
    lastSequentialNumber: number
    balance: number
    profileId: string
    approved: boolean
    pending: boolean
    blocked: boolean
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
    version: number,
    data?: AccountInterface[]
}

export interface AccountsOverviewAction {
    type: string,
    params?: any
    state: AccountsOverviewReducerState,
}
