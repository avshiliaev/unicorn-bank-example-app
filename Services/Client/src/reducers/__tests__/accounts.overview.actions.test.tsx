import accountOverviewService from '../../services/accounts.overview.service';
import { addAccountAsHost, initAccounts } from '../accounts.overview.reducer';
import { AccountInterface } from '../../interfaces/account.interface';
import ActionTypes from '../../constants';

describe('async actions', () => {

  it(ActionTypes.INIT_ACCOUNT, async () => {
    const mockPayload: AccountInterface[] = [
      { uuid: '0x1', title: 't', balance: 0, status: 'approved' }
    ];
    const desiredData: AccountInterface[] = [
      { uuid: '0x1', title: 't', balance: 0, status: 'approved' }
    ];

    accountOverviewService.queryAccounts = jest.fn().mockReturnValue(mockPayload);

    initAccounts('0x1');
    expect(dispatch).toHaveBeenLastCalledWith({
      type: ActionTypes.INIT_ACCOUNT,
      data: desiredData,
    });
  });

  it(ActionTypes.ADD_ACCOUNT, async () => {
    const addAccountInput: AccountInterface = { uuid: '0x1', title: 't', balance: 0, status: 'approved' };

    accountOverviewService.addAccount = jest.fn().mockReturnValue([addAccountInput]);

    const dispatch = jest.fn();
    await addAccountAsHost(addAccountInput);
    expect(dispatch).toHaveBeenLastCalledWith({
      type: ActionTypes.ADD_ACCOUNT,
      data: [addAccountInput],
    });
  });
});
