import {apply, call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import createWebSocketConnection from '../web.socket';
import {NotificationInterface, NotificationsAction} from '../interfaces/notification.interface';
import {createSocketChannel} from './channels';
import * as signalR from "@microsoft/signalr";

interface StreamResponse {
    type: string,
    payload: NotificationInterface[]
}

function* getNotificationsSaga(action) {

    const {params} = action;
    const path = "/notifications";

    let socket: signalR.HubConnection;

    try {

        socket = yield call(createWebSocketConnection, path);
        const socketChannel = yield call(createSocketChannel, socket);

        // TODO: https://github.com/redux-saga/redux-saga/issues/1903
        // TODO: https://stackoverflow.com/questions/60422030/redux-saga-dispatch-return-so-many-requests
        yield apply(socket, signalR.HubConnection.prototype.start, []);
        yield apply(socket, signalR.HubConnection.prototype.invoke, ['Request', params.userId]);

        while (true) {
            const action: StreamResponse = yield take(socketChannel);
            const data = action.payload;
            const type = action.type === 'init'
                ? ActionTypes.QUERY_NOTIFICATIONS_INIT
                : ActionTypes.QUERY_NOTIFICATIONS_UPDATE;
            const actionSuccess: NotificationsAction = {
                type,
                state: {
                    loading: false,
                    error: false,
                    data,
                },
            };
            yield put(actionSuccess);
        }
    } catch (error) {
        const actionError: NotificationsAction = {
            type: ActionTypes.QUERY_NOTIFICATIONS_ERROR,
            state: {
                loading: false,
                error: true,
                data: [],
            },
        };
        yield put(actionError);
    } finally {
        yield apply(socket, signalR.HubConnection.prototype.stop, []);
    }
}

export function* getNotificationsWatcher() {
    yield takeLatest(ActionTypes.QUERY_NOTIFICATIONS, getNotificationsSaga);
}


