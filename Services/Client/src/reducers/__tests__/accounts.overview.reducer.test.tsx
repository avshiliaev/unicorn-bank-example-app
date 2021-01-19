import {
    AccountInterface,
    AccountsOverviewAction,
    AccountsOverviewReducerState
} from '../../interfaces/account.interface';
import {ActionTypes} from "../../constants/constants";
import accountsOverviewReducer, {initAccounts} from "../accounts.overview.reducer";

describe('accountsOverviewReducer', () => {
    it('should return the initial state', () => {
        expect(accountsOverviewReducer(
            undefined,
            {state: undefined, type: ""})
        ).toEqual(
            {
                data: [],
                error: false,
                loading: false,
            }
        );
    });

    it('should handle QUERY_ACCOUNTS', () => {

        // should switch to loading state
        const state: AccountsOverviewReducerState = {error: false, loading: false}
        const action = initAccounts("awesome")
        const newState: AccountsOverviewReducerState = action.state;

        expect(
            accountsOverviewReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });

    it('should handle QUERY_ACCOUNTS_INIT', () => {

        // should switch to init (success) state
        const state: AccountsOverviewReducerState = {error: false, loading: false}
        const data: AccountInterface[] = [
            {
                balance: 0, profile: "awesome", status: "approved"
            }
        ]
        const action: AccountsOverviewAction = {
            type: ActionTypes.QUERY_ACCOUNTS_INIT,
            state: {
                loading: false,
                error: false,
                data,
            }
        }
        const newState: AccountsOverviewReducerState = {...state, ...action.state}

        expect(
            accountsOverviewReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });

    it('should handle GET_ACCOUNT_DETAIL_ERROR', () => {

        // should switch to error state
        const state: AccountsOverviewReducerState = {error: false, loading: false}
        const action: AccountsOverviewAction = {
            type: ActionTypes.QUERY_ACCOUNTS_ERROR,
            state: {
                loading: false,
                error: true,
            }
        }
        const newState: AccountsOverviewReducerState = action.state;

        expect(
            accountsOverviewReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });
});
