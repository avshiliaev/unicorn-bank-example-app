import {apply, call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountAction} from '../interfaces/account.interface';
import {AccountsStreamResponse} from '../interfaces/stream.interface';
import createSocketChannel from "./channels";
import {getAccountError, getAccountSuccess} from "../reducers/account.reducer";
import createClient from "../api/web.socket.api.client";
import {HubConnection} from "@microsoft/signalr";
import {buffers} from "redux-saga";

export function* getAccountDetailSaga(action) {

    const {accountId, token} = action.params;
    const path: string = `${process.env.REACT_APP_PATHS_ACCOUNT_DETAILS ?? "/"}?id=${accountId}`;

    try {
        const socket = yield call(createClient, path, token);
        const socketChannel = yield call(createSocketChannel, path, token, socket, buffers.fixed(100));

        yield apply(socket, HubConnection.prototype.start, []);
        yield apply(socket, HubConnection.prototype.invoke, ['RequestOne']);

        while (true) {
            const response: AccountsStreamResponse = yield take(socketChannel);
            const actionSuccess: AccountAction = getAccountSuccess(response);
            yield put(actionSuccess);
        }
    } catch (error) {
        const actionError: AccountAction = getAccountError();
        yield put(actionError);
    }
}

export function* getAccountDetailWatcher() {
    yield takeLatest(ActionTypes.GET_ACCOUNT_DETAIL, getAccountDetailSaga);
}


