import {call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountInterface, AccountsOverviewAction} from '../interfaces/account.interface';
import {createSocketChannel} from './channels';

interface StreamResponse {
    type: string,
    payload: AccountInterface[]
}

export function* getAccountsSaga(action) {

    const path = "/profiles";

    try {

        const socketChannel = yield call(createSocketChannel, path, "Request");

        while (true) {

            const action: StreamResponse = yield take(socketChannel);
            const data = action.payload;
            const type = action.type === 'init'
                ? ActionTypes.QUERY_ACCOUNTS_INIT
                : ActionTypes.QUERY_ACCOUNTS_UPDATE;
            const actionSuccess: AccountsOverviewAction = {
                type,
                state: {
                    loading: false,
                    error: false,
                    data,
                },
            };
            yield put(actionSuccess);
        }
    } catch (error) {
        const actionError: AccountsOverviewAction = {
            type: ActionTypes.QUERY_ACCOUNTS_ERROR,
            state: {
                loading: false,
                error: true,
                data: [],
            },
        };
        yield put(actionError);
    }
}

export function* getAccountsWatcher() {
    yield takeLatest(ActionTypes.QUERY_ACCOUNTS, getAccountsSaga);
}


