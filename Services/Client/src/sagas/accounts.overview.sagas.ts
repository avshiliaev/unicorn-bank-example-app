import {apply, call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountsOverviewAction} from '../interfaces/account.interface';
import createSocketChannel from './channels';
import {AccountDetailStreamResponse} from "../interfaces/stream.interface";
import {initAccountsError, initAccountsSuccess} from "../reducers/accounts.overview.reducer";
import createClient from "../api/web.socket.api.client";
import {HubConnection} from "@microsoft/signalr";
import {buffers} from "redux-saga";


export function* getAccountsSaga(action) {

    const {token} = action.params;
    const path: string = process.env.REACT_APP_PATHS_PROFILES ?? "/";

    const socket = yield call(createClient, path, token);
    const socketChannel = yield call(createSocketChannel, path, token, socket, buffers.fixed(100));

    yield apply(socket, HubConnection.prototype.start, []);
    yield apply(socket, HubConnection.prototype.invoke, ['RequestAll']);
    try {
        while (true) {
            const response = yield take(socketChannel);

            yield call(console.log, 'Got here 2?');
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


