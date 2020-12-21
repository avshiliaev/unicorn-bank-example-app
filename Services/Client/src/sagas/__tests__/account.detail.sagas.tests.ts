import {call} from 'redux-saga/effects';
import {expectSaga} from 'redux-saga-test-plan';
import * as matchers from 'redux-saga-test-plan/matchers';
import {throwError} from 'redux-saga-test-plan/providers';
import {getAccountDetailSaga} from "../account.detail.sagas";
import {getAccount} from "../../reducers/account.reducer";
import {AccountAction, AccountInterface} from "../../interfaces/account.interface";
import createWebSocketConnection from "../../web.socket";
import {createSocketChannel} from "../api";
import {ActionTypes} from "../../constants";

// TODO: based on the example https://github.com/jfairbank/redux-saga-test-plan#mocking-with-providers
describe('getAccountDetailSaga', () => {
        it('Gets an account via Websocket', () => {

            // Arrange
            const getAccountAction: AccountAction = getAccount("awesome")
            const socket = createWebSocketConnection("path")
            const socketChannel = createSocketChannel(socket)

            const mockAccount: AccountInterface = {balance: 0, profile: "awesome", status: "approved"}
            const actionSuccess: AccountAction = {
                type: ActionTypes.GET_ACCOUNT_DETAIL_SUCCESS,
                state: {
                    loading: false,
                    error: false,
                    data: mockAccount,
                },
            };

            // Act / Assert
            return expectSaga(getAccountDetailSaga, getAccountAction)
                // Double yield call
                .provide([
                    [call(createWebSocketConnection, "path"), socket],
                    [matchers.call.fn(createSocketChannel), socketChannel],
                ])
                .take(socketChannel)
                .put(actionSuccess)
                .dispatch(getAccountAction)
                .run(false);
        });

        it('handles errors', () => {

            // Arrange
            const getAccountAction: AccountAction = getAccount("awesome")
            const socket = createWebSocketConnection("path")
            const socketChannel = createSocketChannel(socket)

            const actionError: AccountAction = {
                type: ActionTypes.GET_ACCOUNT_DETAIL_ERROR,
                state: {
                    loading: false,
                    error: true,
                },
            };

            // Act / Assert
            return expectSaga(getAccountDetailSaga, getAccountAction)
                // Double yield call
                .provide([
                    [call(createWebSocketConnection, "path"), socket],
                    [matchers.call.fn(createSocketChannel), socketChannel],
                ])
                .take(socketChannel)
                .put(actionError)
                .dispatch(getAccountAction)
                .run(false);
        });
    }
)
