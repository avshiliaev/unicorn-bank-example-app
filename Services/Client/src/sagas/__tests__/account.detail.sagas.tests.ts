import {testSaga} from 'redux-saga-test-plan';
import {ActionTypes} from "../../constants";
import createSocketChannel from "../channels";
import {AccountAction} from "../../interfaces/account.interface";
import {getAccountsSaga} from "../accounts.overview.sagas";
import {getAccount} from "../../reducers/account.reducer";
import {getAccountDetailSaga} from "../account.detail.sagas";

jest.mock('../channels', () => require('../__mocks__/channels'));

describe('getAccountsSaga', () => {

        it('puts error effect', () => {

            const accountId = "awesome";
            const token = "token";
            const path = `/accounts?id=${accountId}`;
            const getAccountAction: AccountAction = getAccount(accountId, token)
            const actionError: AccountAction = {
                type: ActionTypes.GET_ACCOUNT_DETAIL_ERROR,
                state: {
                    loading: false,
                    error: true,
                },
            };

            testSaga(getAccountDetailSaga, getAccountAction)
                .next()
                .call(createSocketChannel, path, token, "Request")
                .next()
                .put(actionError)
                .next()
                .isDone();
        });
    }
)
