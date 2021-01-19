import accountReducer, {getAccount} from '../account.reducer';
import {AccountAction, AccountInterface, AccountReducerState} from '../../interfaces/account.interface';
import {ActionTypes} from "../../constants/constants";

describe('accountReducer', () => {
    it('should return the initial state', () => {
        expect(accountReducer(undefined, {state: undefined, type: ""})).toEqual({});
    });

    it('should handle GET_ACCOUNT_DETAIL', () => {

        // should switch to loading state
        const state: AccountReducerState = {error: false, loading: false}
        const action = getAccount("awesome", "token")
        const newState: AccountReducerState = action.state;

        expect(
            accountReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });

    it('should handle GET_ACCOUNT_DETAIL_SUCCESS', () => {

        // should switch to success state
        const state: AccountReducerState = {error: false, loading: false}
        const data: AccountInterface = {
            balance: 0, profile: "awesome", status: "approved"
        }
        const action: AccountAction = {
            type: ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS,
            state: {
                loading: false,
                error: false,
                data,
            }
        }
        const newState: AccountReducerState = {...state, ...action.state}

        expect(
            accountReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });

    it('should handle GET_ACCOUNT_DETAIL_ERROR', () => {

        // should switch to error state
        const state: AccountReducerState = {error: false, loading: false}
        const action: AccountAction = {
            type: ActionTypes.GET_ACCOUNT_DETAIL_ERROR,
            state: {
                loading: false,
                error: true,
            }
        }
        const newState: AccountReducerState = action.state;

        expect(
            accountReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });
});
