import {AccountAction, AccountInterface, AccountReducerState} from '../interfaces/account.interface';
import {ActionTypes} from '../constants';

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

const getAccountSuccess = (response: AccountInterface): AccountAction => {
    const data = response;
    const type = ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS;
    return {
        type,
        state: {
            loading: false,
            error: false,
            data,
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
