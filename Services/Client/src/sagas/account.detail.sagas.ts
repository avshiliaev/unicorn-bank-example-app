import {call, fork, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountAction} from '../interfaces/account.interface';
import {AccountsStreamResponse} from '../interfaces/stream.interface';
import {createSocketChannel, invokeSocket} from "./channels";
import {getAccountError, getAccountSuccess} from "../reducers/account.reducer";
import createClient from "../api/web.socket.api.client";
import {buffers} from "redux-saga";

export function* getAccountDetailSaga(action) {

    const {accountId, token} = action.params;
    const path: string = `${process.env.REACT_APP_PATHS_ACCOUNT_DETAILS ?? "/"}?id=${accountId}`;

    const socket = yield call(createClient, path, token);
    const socketChannel = yield call(createSocketChannel, path, token, socket, buffers.fixed(100));

    yield fork(invokeSocket, socket, "RequestOne");

    while (true) {
        try {
            const response: AccountsStreamResponse = yield take(socketChannel);
            const actionSuccess: AccountAction = getAccountSuccess(response);
            yield put(actionSuccess);

        } catch (error) {
            const actionError: AccountAction = getAccountError();
            yield put(actionError);
        }
    }

}

export function* getAccountDetailWatcher() {
    yield takeLatest(ActionTypes.GET_ACCOUNT_DETAIL, getAccountDetailSaga);
}


