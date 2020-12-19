import { call, put, take, takeLatest } from 'redux-saga/effects';
import { ActionTypes } from '../constants';
import createWebSocketConnection from '../web.socket';
import { NotificationInterface, NotificationsAction } from '../interfaces/notification.interface';
import { createSocketChannel } from './api';

interface StreamResponse {
  type: string,
  payload: NotificationInterface[]
}

function* getNotificationsSaga(action) {

  const { params } = action;
  const path = `/notifications?profile=${params.userId}&count=${params.count}`;

  const socket = yield call(createWebSocketConnection, path);
  const socketChannel = yield call(createSocketChannel, socket);

  try {
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
  }
}

export function* getNotificationsWatcher() {
  yield takeLatest(ActionTypes.QUERY_NOTIFICATIONS, getNotificationsSaga);
}


