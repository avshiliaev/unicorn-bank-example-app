import {apply, call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {NotificationsAction} from '../interfaces/notification.interface';
import createSocketChannel from './channels';
import {NotificationStreamResponse} from "../interfaces/stream.interface";
import {initNotificationsError, initNotificationsSuccess} from "../reducers/notifications.reducer";
import createClient from "../api/web.socket.api.client";
import {HubConnection} from "@microsoft/signalr";
import {buffers} from "redux-saga";


export function* getNotificationsSaga(action) {

    const {token} = action.params;
    const path: string = process.env.REACT_APP_PATHS_ACCOUNT_NOTIFICATIONS ?? "/";

    try {
        const socket = yield call(createClient, path, token);
        const socketChannel = yield call(createSocketChannel, path, token, socket, buffers.fixed(100));

        yield apply(socket, HubConnection.prototype.start, []);
        yield apply(socket, HubConnection.prototype.invoke, ['RequestAll']);

        while (true) {
            const response: NotificationStreamResponse = yield take(socketChannel);
            const actionSuccess: NotificationsAction = initNotificationsSuccess(response);
            yield put(actionSuccess);
        }
    } catch (error) {
        const actionError: NotificationsAction = initNotificationsError();
        yield put(actionError);
    }
}

export function* getNotificationsWatcher() {
    yield takeLatest(ActionTypes.QUERY_NOTIFICATIONS, getNotificationsSaga);
}


