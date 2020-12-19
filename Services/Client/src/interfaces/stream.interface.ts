import {AccountInterface} from './account.interface';

export interface AccountsStreamResponse {
    type: string,
    payload: AccountInterface[]
}
