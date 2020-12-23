import {call} from 'redux-saga/effects';
import {expectSaga} from 'redux-saga-test-plan';
import * as matchers from 'redux-saga-test-plan/matchers';
import createWebSocketConnection from "../../web.socket";
import {ActionTypes} from "../../constants";
import {createSocketChannel} from "../channels";
import {AccountInterface, AccountsOverviewAction} from "../../interfaces/account.interface";
import {initAccounts} from "../../reducers/accounts.overview.reducer";
import {getAccountsSaga} from "../accounts.overview.sagas";

describe('getAccountsSaga', () => {
        it('Gets accounts overview via Websocket', () => {

            // Arrange
            const queryAccountsAction: AccountsOverviewAction = initAccounts(
                "awesome"
            )
            const socket = createWebSocketConnection("path")
            const socketChannel = createSocketChannel(socket)

            const mockAccountsOverview: AccountInterface = {
                balance: 0,
                profile: "profile",
                status: "status"
            }
            const initAccountsSuccess: AccountsOverviewAction = {
                type: ActionTypes.QUERY_ACCOUNTS_INIT,
                state: {
                    loading: false,
                    error: false,
                    data: [mockAccountsOverview],
                },
            };

            // Act / Assert
            return expectSaga(getAccountsSaga, queryAccountsAction)
                // Double yield call
                .provide([
                    [call(createWebSocketConnection, "path"), socket],
                    [matchers.call.fn(createSocketChannel), socketChannel],
                ])
                .take(socketChannel)
                .put(initAccountsSuccess)
                .dispatch(queryAccountsAction)
                .run(false);
        });

        it('handles errors', () => {

            // Arrange
            const queryAccountsAction: AccountsOverviewAction = initAccounts(
                "awesome"
            )
            const socket = createWebSocketConnection("path")
            const socketChannel = createSocketChannel(socket)

            const initAccountsError: AccountsOverviewAction = {
                type: ActionTypes.QUERY_ACCOUNTS_ERROR,
                state: {
                    loading: false,
                    error: true,
                },
            };

            // Act / Assert
            return expectSaga(getAccountsSaga, queryAccountsAction)
                // Double yield call
                .provide([
                    [call(createWebSocketConnection, "path"), socket],
                    [matchers.call.fn(createSocketChannel), socketChannel],
                ])
                .take(socketChannel)
                .put(initAccountsError)
                .dispatch(queryAccountsAction)
                .run(false);
        });
    }
)
