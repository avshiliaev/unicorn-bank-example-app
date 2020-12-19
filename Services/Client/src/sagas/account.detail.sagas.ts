import {call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountAction} from '../interfaces/account.interface';
import createWebSocketConnection from '../web.socket';
import {AccountsStreamResponse} from '../interfaces/stream.interface';
import {createSocketChannel} from './api';

function* getAccountDetailSaga(action) {

    const {params} = action;
    const path = `/profiles?account=${params}`;

    const socket = yield call(createWebSocketConnection, path);
    const socketChannel = yield call(createSocketChannel, socket);

    // TODO move all function defining actions to ONE place!
    try {
        while (true) {
            const action: AccountsStreamResponse = yield take(socketChannel);
            const data = action.payload;
            const type = action.type === 'init'
                ? ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS
                : ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS;
            const actionSuccess: AccountAction = {
                type,
                state: {
                    loading: false,
                    error: false,
                    data: data[0],
                },
            };
            yield put(actionSuccess);
        }
    } catch (error) {
        const actionError: AccountAction = {
            type: ActionTypes.GET_ACCOUNT_DETAIL_ERROR,
            state: {
                loading: false,
                error: true,
            },
        };
        yield put(actionError);
    }
}

export function* getAccountDetailWatcher() {
    yield takeLatest(ActionTypes.GET_ACCOUNT_DETAIL, getAccountDetailSaga);
}


