import { AccountAction, AccountInterface, AccountReducerState } from '../interfaces/account.interface';
import { ActionTypes } from '../constants';

const getAccount = (accountId: string): AccountAction => {
  return {
    type: ActionTypes.GET_ACCOUNT_DETAIL,
    params: accountId,
    state: {
      loading: true,
      error: false,
    },
  };
};

export { getAccount };

const accountReducer = (
  state: AccountReducerState,
  action: AccountAction,
): AccountInterface | Object => {
  switch (action.type) {
    case ActionTypes.GET_ACCOUNT_DETAIL:
      return { ...state, ...action.state };
    case ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS:
      return { ...state, ...action.state };
    case ActionTypes.GET_ACCOUNT_DETAIL_ERROR:
      return { ...state, ...action.state };
    default:
      return { ...state };
  }
};

export default accountReducer;
