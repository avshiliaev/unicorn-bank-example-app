import {getAccount} from '../account.reducer';
import {AccountInterface} from '../../interfaces/account.interface';
import {ActionTypes} from "../../constants";

describe('async actions', () => {

    it(ActionTypes.GET_ACCOUNT_DETAIL, async () => {
        const account: AccountInterface = {profile: "awesome", uuid: '0x1', balance: 0, status: 'approved'};

        const dispatch = jest.fn();
        await getAccount('0x9')(dispatch);
        expect(dispatch).toHaveBeenLastCalledWith({
            type: ActionTypes.GET_ACCOUNT,
            data: account,
        });
    });
});
