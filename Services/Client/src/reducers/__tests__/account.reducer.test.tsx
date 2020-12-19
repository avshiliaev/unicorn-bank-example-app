import accountReducer, {getAccount} from '../account.reducer';
import {AccountReducerState} from '../../interfaces/account.interface';

describe('accountReducer', () => {
    it('should return the initial state', () => {
        expect(accountReducer(undefined, {state: undefined, type: ""})).toEqual({});
    });

    it('should handle GET_ACCOUNT', () => {

        // should switch to loading state
        const state: AccountReducerState = {error: false, loading: false}
        const action = getAccount("awesome")
        const newState: AccountReducerState = {error: false, loading: true}

        expect(
            accountReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });
});
