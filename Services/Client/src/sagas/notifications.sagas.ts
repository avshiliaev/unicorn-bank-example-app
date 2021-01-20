import {call, fork, put, take, takeLatest} from 'redux-saga/effects';
import {NotificationInterface, NotificationsAction} from '../interfaces/notification.interface';
import {createSocketChannel, invokeSocket, SocketChannelProps} from './channels';
import {initNotificationsError, initNotificationsSuccess} from "../reducers/notifications.reducer";
import createClient from "../api/web.socket.api.client";
import {buffers} from "redux-saga";
import {ActionTypes} from "../constants/action.types";
import {Environment} from "../constants/environment";


export function* getNotificationsSaga(action) {

    const {token, count} = action.params;
    const path: string = Environment.PATHS_NOTIFICATIONS;

    const socket = yield call(createClient, path, token);

    const channelProps: SocketChannelProps = {
        path,
        socket,
        token,
        responseMethod: "ResponseAll",
        buffer: buffers.fixed(100),
    }
    const socketChannel = yield call(createSocketChannel, channelProps);

    yield fork(invokeSocket, socket, ["RequestAll", count]);

    while (true) {
        try {
            const response: NotificationInterface[] = yield take(socketChannel);
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
    yield takeLatest(ActionTypes.UPDATE_VIEW_SETTINGS_NOTIFICATIONS_COUNT, getNotificationsSaga);
}
