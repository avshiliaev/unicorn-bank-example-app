import {call, fork, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountInterface, AccountsOverviewAction} from '../interfaces/account.interface';
import {createSocketChannel, invokeSocket} from './channels';
import {initAccountsError, initAccountsSuccess} from "../reducers/accounts.overview.reducer";
import createClient from "../api/web.socket.api.client";
import {buffers} from "redux-saga";


export function* getAccountsSaga(action) {

    const {token} = action.params;
    const path: string = process.env.REACT_APP_PATHS_PROFILES ?? "/";

    const socket = yield call(createClient, path, token);
    const socketChannel = yield call(createSocketChannel, path, token, socket, buffers.fixed(100));

    yield fork(invokeSocket, socket, "RequestAll");

    while (true) {
        try {
            const response: AccountInterface[] = yield take(socketChannel);
            const actionSuccess: AccountsOverviewAction = initAccountsSuccess(response);
            yield put(actionSuccess);
        } catch (error) {
            const actionError: AccountsOverviewAction = initAccountsError();
            yield put(actionError);
        }
    }
}

export function* getAccountsWatcher() {
    yield takeLatest(ActionTypes.QUERY_ACCOUNTS, getAccountsSaga);
}


