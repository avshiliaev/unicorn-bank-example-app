import { getAccount } from '../account.reducer';
import accountService from '../../services/account.detail.service';
import { AccountInterface } from '../../interfaces/account.interface';
import ActionTypes from '../../constants';

describe('async actions', () => {

  it(ActionTypes.GET_ACCOUNT, async () => {
    const account: AccountInterface = { uuid: '0x1', title: 't', balance: 0, status: 'approved' };

    accountService.queryAccount = jest.fn().mockReturnValue(account);

    const dispatch = jest.fn();
    await getAccount('0x9')(dispatch);
    expect(dispatch).toHaveBeenLastCalledWith({
      type: ActionTypes.GET_ACCOUNT,
      data: account,
    });
  });
});
