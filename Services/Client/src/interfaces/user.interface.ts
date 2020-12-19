import { AccountInterface } from './account.interface';

// General
export interface UserInterface {
  id?: string
  username: string
  accounts?: AccountInterface[]
}

// User Detail View
export interface UserReducerState {
  loading: boolean
  error: boolean
  data?: UserInterface
}

export interface UserAction {
  type: string
  params?: any
  state: UserReducerState
}
