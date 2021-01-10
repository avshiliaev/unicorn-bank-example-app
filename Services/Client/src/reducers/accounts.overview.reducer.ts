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
            version: 0,
            data: [],
        },
    };
};

const initAccountsSuccess = (response): AccountsOverviewAction => {
    const data = response;
    const type = ActionTypes.QUERY_ACCOUNTS_SUCCESS;
    return {
        type,
        state: {
            loading: false,
            error: false,
            version: 1,
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
            version: 1,
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
            version: 1,
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
    version: 1,
    data: [],
};

const accountsOverviewReducer = (
    state: AccountsOverviewReducerState = accountsOverviewInitialState,
    action: AccountsOverviewAction,
): AccountsOverviewReducerState => {
    switch (action.type) {

        case ActionTypes.QUERY_ACCOUNTS:
            return action.state;

        // TODO: clean up!
        case ActionTypes.QUERY_ACCOUNTS_SUCCESS:

            if (state.version <= 1) {
                action.state.version += state.version;
                return action.state;
            }

            action.state.version += state.version;
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
