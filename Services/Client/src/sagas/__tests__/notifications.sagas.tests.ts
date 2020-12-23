import {call} from 'redux-saga/effects';
import {expectSaga} from 'redux-saga-test-plan';
import * as matchers from 'redux-saga-test-plan/matchers';
import createWebSocketConnection from "../../web.socket";
import {ActionTypes} from "../../constants";
import {createSocketChannel} from "../channels";
import {initNotifications} from "../../reducers/notifications.reducer";
import {NotificationInterface, NotificationsAction} from "../../interfaces/notification.interface";
import {getNotificationsSaga} from "../notifications.sagas";
import {createMockSocket} from "./__mocks__/socket.mock";
import {HubConnection} from "@microsoft/signalr";

describe('getNotificationsSaga', () => {

        it('Gets notifications via Websocket', () => {

            // Arrange
            const queryNotificationsAction: NotificationsAction = initNotifications(
                "awesome",
                5
            )
            const socket = createWebSocketConnection("path")
            const socketChannel = createSocketChannel(socket)

            const mockNotifications: NotificationInterface = {
                description: "description",
                profile: "awesome",
                status: "status",
                time: "time",
                title: "title",
                uuid: "uuid"
            }
            const initNotificationSuccess: NotificationsAction = {
                type: ActionTypes.QUERY_NOTIFICATIONS_INIT,
                state: {
                    loading: false,
                    error: false,
                    data: [mockNotifications],
                },
            };

            // Act / Assert
            return expectSaga(getNotificationsSaga, queryNotificationsAction)
                // Double yield call
                .provide([
                    [call(createWebSocketConnection, "path"), socket],
                    [matchers.call.fn(createSocketChannel), socketChannel],
                ])
                .take(socketChannel)
                .put(initNotificationSuccess)
                .dispatch(queryNotificationsAction)
                .run(false);
        });

        it('handles errors', () => {

            // Arrange
            const queryNotificationsAction: NotificationsAction = initNotifications(
                "awesome",
                5
            )

            const mockSocket = createMockSocket();
            const socketChannel = createSocketChannel(mockSocket)

            const initNotificationError: NotificationsAction = {
                type: ActionTypes.QUERY_NOTIFICATIONS_ERROR,
                state: {
                    loading: false,
                    error: true,
                },
            };

            // Act / Assert
            return expectSaga(getNotificationsSaga, queryNotificationsAction)
                // Double yield call
                .provide([
                    [call(createWebSocketConnection, "/notifications"), mockSocket],
                    [matchers.call.fn(createSocketChannel), socketChannel],
                    [matchers.apply(mockSocket, HubConnection.prototype.start, []), {}],
                    [matchers.apply(mockSocket, HubConnection.prototype.invoke, ['Request', "awesome"]), {}],
                ])
                // .apply(mockSocket, HubConnection.prototype.start, [])
                // .apply(mockSocket, HubConnection.prototype.invoke, ['Request', "awesome"])
                .take(socketChannel)
                .put(initNotificationError)
                .dispatch(queryNotificationsAction)
                .run(false);
        });
    }
)
