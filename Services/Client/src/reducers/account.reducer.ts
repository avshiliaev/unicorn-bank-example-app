import {AccountAction, AccountInterface, AccountReducerState} from '../interfaces/account.interface';
import {ActionTypes} from '../constants';
import {AccountsStreamResponse} from "../interfaces/stream.interface";

const getAccount = (accountId: string, token: string): AccountAction => {
    return {
        type: ActionTypes.GET_ACCOUNT_DETAIL,
        params: {accountId, token},
        state: {
            loading: true,
            error: false,
        },
    };
};

const getAccountSuccess = (response: AccountsStreamResponse): AccountAction => {
    const data = response.payload;
    const type = response.type === 'init'
        ? ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS
        : ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS;
    return  {
        type,
        state: {
            loading: false,
            error: false,
            data: data[0],
        },
    };
}

const getAccountError = (): AccountAction => {
    return {
        type: ActionTypes.GET_ACCOUNT_DETAIL_ERROR,
        state: {
            loading: false,
            error: true,
        },
    }
}

export {getAccount, getAccountSuccess, getAccountError};

const accountReducer = (
    state: AccountReducerState,
    action: AccountAction,
): AccountInterface | Object => {
    switch (action.type) {
        case ActionTypes.GET_ACCOUNT_DETAIL:
            return {...state, ...action.state};
        case ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS:
            return {...state, ...action.state};
        case ActionTypes.GET_ACCOUNT_DETAIL_ERROR:
            return {...state, ...action.state};
        default:
            return {...state};
    }
};

export default accountReducer;
