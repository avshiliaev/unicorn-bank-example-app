import {AccountInterface} from './account.interface';
import {NotificationInterface} from "./notification.interface";

export interface GenericStreamObject {
    type: string,
    payload?: any[],
}

export interface AccountsStreamResponse extends GenericStreamObject {
    payload: AccountInterface[]
}

export interface NotificationStreamResponse extends GenericStreamObject {
    payload: NotificationInterface[]
}

export interface AccountDetailStreamResponse extends GenericStreamObject {
    payload: AccountInterface[]
}
