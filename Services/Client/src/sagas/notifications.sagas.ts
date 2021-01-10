import {call, fork, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {NotificationsAction} from '../interfaces/notification.interface';
import {createSocketChannel, invokeSocket} from './channels';
import {NotificationStreamResponse} from "../interfaces/stream.interface";
import {initNotificationsError, initNotificationsSuccess} from "../reducers/notifications.reducer";
import createClient from "../api/web.socket.api.client";
import {buffers} from "redux-saga";


export function* getNotificationsSaga(action) {

    const {token} = action.params;
    const path: string = process.env.REACT_APP_PATHS_ACCOUNT_NOTIFICATIONS ?? "/";

    const socket = yield call(createClient, path, token);
    const socketChannel = yield call(createSocketChannel, path, token, socket, buffers.fixed(100));

    yield fork(invokeSocket, socket, "RequestOne");

    while (true) {
        try {
            const response: NotificationStreamResponse = yield take(socketChannel);
            const actionSuccess: NotificationsAction = initNotificationsSuccess(response);
            yield put(actionSuccess);


        } catch (error) {
            const actionError: NotificationsAction = initNotificationsError();
            yield put(actionError);
        }
    }

}

export function* getNotificationsWatcher() {
    yield takeLatest(ActionTypes.QUERY_NOTIFICATIONS, getNotificationsSaga);
}


