import {testSaga} from 'redux-saga-test-plan';
import {ActionTypes} from "../../constants";
import createSocketChannel from "../channels";
import {AccountsOverviewAction} from "../../interfaces/account.interface";
import {initAccounts} from "../../reducers/accounts.overview.reducer";
import {getAccountsSaga} from "../accounts.overview.sagas";

jest.mock('../channels', () => require('../__mocks__/channels'));

describe('getAccountsSaga', () => {

        it('puts error effect', () => {

            const userId = "awesome";
            const path = "/profiles";
            const queryAccountsAction: AccountsOverviewAction = initAccounts(userId)
            const actionError: AccountsOverviewAction = {
                type: ActionTypes.QUERY_ACCOUNTS_ERROR,
                state: {
                    loading: false,
                    error: true,
                    data: [],
                },
            };

            testSaga(getAccountsSaga, queryAccountsAction)
                .next()
                .call(createSocketChannel, path, "Request")
                .next()
                .put(actionError)
                .next()
                .isDone();
        });
    }
)
