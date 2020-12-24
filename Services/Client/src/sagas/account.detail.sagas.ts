import {call, put, take, takeLatest} from 'redux-saga/effects';
import {ActionTypes} from '../constants';
import {AccountAction} from '../interfaces/account.interface';
import {AccountsStreamResponse} from '../interfaces/stream.interface';
import createSocketChannel from "./channels";
import {getAccountError, getAccountSuccess} from "../reducers/account.reducer";

export function* getAccountDetailSaga(action) {

    const {accountId, token} = action.params;
    const path: string = `${process.env.REACT_APP_PATHS_ACCOUNT_DETAILS?? "/"}?id=${accountId}`;

    try {
        const socketChannel = yield call(createSocketChannel, path, token, "Request");
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


