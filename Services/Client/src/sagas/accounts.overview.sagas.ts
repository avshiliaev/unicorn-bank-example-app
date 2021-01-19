import {call, fork, put, take, takeLatest} from 'redux-saga/effects';
import {AccountInterface, AccountsOverviewAction} from '../interfaces/account.interface';
import {createSocketChannel, invokeSocket, SocketChannelProps} from './channels';
import {initAccountsError, initAccountsSuccess} from "../reducers/accounts.overview.reducer";
import createClient from "../api/web.socket.api.client";
import {buffers} from "redux-saga";
import {ActionTypes} from "../constants/action.types";
import {Environment} from "../constants/environment";


export function* getAccountsSaga(action) {

    const {token} = action.params;
    const path: string = Environment.PATHS_PROFILES;

    const socket = yield call(createClient, path, token);

    const channelProps: SocketChannelProps = {
        path,
        socket,
        token,
        responseMethod: "ResponseAll",
        buffer: buffers.fixed(100),
    }
    const socketChannel = yield call(createSocketChannel, channelProps);

    yield fork(invokeSocket, socket, ["RequestAll"]);

    while (true) {
        try {
            const response: AccountInterface[] = yield take(socketChannel);
            console.log(response)
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


