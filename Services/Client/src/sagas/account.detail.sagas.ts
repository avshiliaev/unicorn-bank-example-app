import {call, fork, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountAction, AccountInterface} from '../interfaces/account.interface';
import {createSocketChannel, invokeSocket, SocketChannelProps} from "./channels";
import {getAccountError, getAccountSuccess} from "../reducers/account.reducer";
import createClient from "../api/web.socket.api.client";
import {buffers} from "redux-saga";

export function* getAccountDetailSaga(action) {

    const {accountId, token} = action.params;
    const path: string = process.env.REACT_APP_PATHS_PROFILES ?? "/";

    const socket = yield call(createClient, path, token);

    const channelProps: SocketChannelProps = {
        path,
        socket,
        token,
        responseMethod: "ResponseOne",
        buffer: buffers.fixed(100),
    }
    const socketChannel = yield call(createSocketChannel, channelProps);

    yield fork(invokeSocket, socket, ["RequestOne", accountId]);

    while (true) {
        try {
            const response: AccountInterface = yield take(socketChannel);
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


