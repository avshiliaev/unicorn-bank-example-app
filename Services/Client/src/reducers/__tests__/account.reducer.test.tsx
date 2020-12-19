import accountReducer from '../account.reducer';
import {AccountInterface} from '../../interfaces/account.interface';
import ActionTypes from '../../constants';

describe('accountReducer', () => {
    it('should return the initial state', () => {
        expect(accountReducer(undefined, {})).toEqual({});
    });

    it('should handle GET_ACCOUNT', () => {
        const accountOne: AccountInterface = {uuid: '1', title: 't', balance: 0, status: 'approved'};
        const accountTwo: AccountInterface = {uuid: '2', title: 't', balance: 1, status: 'approved'};

        expect(
            accountReducer(
                {},
                {
                    type: ActionTypes.GET_ACCOUNT,
                    data: accountOne,
                }),
        ).toEqual(accountOne);

        expect(
            accountReducer(
                accountOne,
                {
                    type: ActionTypes.GET_ACCOUNT,
                    data: accountTwo,
                },
            ),
        ).toEqual(accountTwo);
    });
});
