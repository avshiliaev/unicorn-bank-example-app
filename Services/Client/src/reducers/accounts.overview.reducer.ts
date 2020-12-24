import {AccountInterface, AccountsOverviewAction, AccountsOverviewReducerState} from '../interfaces/account.interface';
import {ActionTypes} from '../constants';
import {AccountDetailStreamResponse} from "../interfaces/stream.interface";

const initAccounts = (token: string): AccountsOverviewAction => {
    return {
        type: ActionTypes.QUERY_ACCOUNTS,
        params: {token},
        state: {
            loading: true,
            error: false,
            data: [],
        },
    };
};

const initAccountsSuccess = (response: AccountDetailStreamResponse): AccountsOverviewAction => {
    const data = response.payload;
    const type = response.type === 'init'
        ? ActionTypes.QUERY_ACCOUNTS_INIT
        : ActionTypes.QUERY_ACCOUNTS_UPDATE;
    return {
        type,
        state: {
            loading: false,
            error: false,
            data,
        },
    };
}

const initAccountsError = (): AccountsOverviewAction => {
    return {
        type: ActionTypes.QUERY_ACCOUNTS_ERROR,
        state: {
            loading: false,
            error: true,
            data: [],
        },
    };
}

// TODO: add saga for adding a new account
const addAccountAsHost = (account: AccountInterface): AccountsOverviewAction => {
    return {
        type: ActionTypes.ADD_ACCOUNT,
        state: {
            loading: true,
            error: false,
            data: [account],
        },
    };
};

export {
    initAccounts,
    initAccountsSuccess,
    initAccountsError,
    addAccountAsHost,
};

const accountsOverviewInitialState: AccountsOverviewReducerState = {
    loading: false,
    error: false,
    data: [],
};

const accountsOverviewReducer = (
    state: AccountsOverviewReducerState = accountsOverviewInitialState,
    action: AccountsOverviewAction,
): AccountsOverviewReducerState => {
    switch (action.type) {

        case ActionTypes.QUERY_ACCOUNTS:
            return {...state, ...action.state};

        case ActionTypes.QUERY_ACCOUNTS_INIT:
            // TODO the array gets overwritten!
            return {...state, ...action.state};

        // TODO: clean up!
        case ActionTypes.QUERY_ACCOUNTS_UPDATE:
            const update = action.state.data[0];

            // Update or add new one
            if (state.data.filter(a => a.uuid === update.uuid).length > 0) {
                const data = state.data.map(account => {
                    if (account.uuid === update.uuid) {
                        return {...account, ...update};
                    }
                    return account;
                });
                return {...state, data};
            } else {
                const data = [...state.data, update];
                return {...state, data};
            }
        case ActionTypes.QUERY_ACCOUNTS_ERROR:
            return {...state, ...action.state};

        case ActionTypes.ADD_ACCOUNT:
            return {...state, data: [...state.data, ...action.state.data]};

        default:
            return state;
    }
};

export default accountsOverviewReducer;
