import {testSaga} from 'redux-saga-test-plan';
import {ActionTypes} from "../../constants";
import createSocketChannel from "../channels";
import {initNotifications} from "../../reducers/notifications.reducer";
import {NotificationsAction} from "../../interfaces/notification.interface";
import {getNotificationsSaga} from "../notifications.sagas";

jest.mock('../channels', () => require('../__mocks__/channels'));

describe('getNotificationsSaga', () => {

        it('puts error effect', () => {

            const userId = "awesome";
            const path = `/notifications`;
            const queryNotificationsAction: NotificationsAction = initNotifications(userId, 5)
            const actionError: NotificationsAction = {
                type: ActionTypes.QUERY_NOTIFICATIONS_ERROR,
                state: {
                    loading: false,
                    error: true,
                    data: [],
                },
            };

            testSaga(getNotificationsSaga, queryNotificationsAction)
                .next()
                .call(createSocketChannel, path, "Request")
                .next()
                .put(actionError)
                .next()
                .isDone();
        });
    }
)
