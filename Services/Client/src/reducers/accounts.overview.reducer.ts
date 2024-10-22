import {AccountInterface, AccountsOverviewAction, AccountsOverviewReducerState} from '../interfaces/account.interface';
import {ActionTypes} from "../constants/action.types";

const initAccounts = (token: string, count: number): AccountsOverviewAction => {
    return {
        type: ActionTypes.QUERY_ACCOUNTS,
        params: {token, count},
        state: {
            loading: true,
            error: false,
            version: 1,
            data: [],
        },
    };
};

const initAccountsSuccess = (response: AccountInterface[]): AccountsOverviewAction => {
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
    version: 0,
    data: [],
};

const accountsOverviewReducer = (
    state: AccountsOverviewReducerState = accountsOverviewInitialState,
    action: AccountsOverviewAction,
): AccountsOverviewReducerState => {
    switch (action.type) {

        case ActionTypes.QUERY_ACCOUNTS:
            return action.state;

        case ActionTypes.QUERY_ACCOUNTS_SUCCESS:

            // If the initial data
            if (state.version === 1) {
                action.state.version += state.version;
                return action.state;
            }
            // If an update
            action.state.version += state.version;
            const update = action.state.data;
            const filtered = state.data.filter(function (objFromA) {
                return !update.find(function (objFromB) {
                    return objFromA.accountId === objFromB.accountId
                })
            })
            return {...action.state, data: [...filtered, ...update]}

        case ActionTypes.QUERY_ACCOUNTS_ERROR:
            return {...state, ...action.state};

        case ActionTypes.ADD_ACCOUNT:
            return {...state, data: [...state.data, ...action.state.data]};

        default:
            return state;
    }
};

export default accountsOverviewReducer;
