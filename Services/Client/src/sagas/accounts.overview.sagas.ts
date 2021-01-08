import {call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountsOverviewAction} from '../interfaces/account.interface';
import createSocketChannel from './channels';
import {AccountDetailStreamResponse} from "../interfaces/stream.interface";
import {initAccountsError, initAccountsSuccess} from "../reducers/accounts.overview.reducer";


export function* getAccountsSaga(action) {

    const {token} = action.params;
    const path: string = process.env.REACT_APP_PATHS_PROFILES ?? "/";

    try {
        const socketChannel = yield call(createSocketChannel, path, token, "RequestAll");

        while (true) {
            const response: AccountDetailStreamResponse = yield take(socketChannel);
            const actionSuccess: AccountsOverviewAction = initAccountsSuccess(response);
            yield put(actionSuccess);
        }
    } catch (error) {
        const actionError: AccountsOverviewAction = initAccountsError();
        yield put(actionError);
    }
}

export function* getAccountsWatcher() {
    yield takeLatest(ActionTypes.QUERY_ACCOUNTS, getAccountsSaga);
}


