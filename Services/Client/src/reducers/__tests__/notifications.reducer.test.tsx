import {ActionTypes} from "../../constants";
import notificationsReducer, {initNotifications} from "../notifications.reducer";
import {
    NotificationInterface,
    NotificationsAction,
    NotificationsReducerState
} from "../../interfaces/notification.interface";

describe('notificationsReducer', () => {
    it('should return the initial state', () => {
        expect(notificationsReducer(
            undefined,
            {state: undefined, type: ""})
        ).toEqual(
            {
                data: [],
                error: false,
                loading: false,
            }
        );
    });

    it('should handle QUERY_NOTIFICATIONS', () => {

        // should switch to loading state
        const state: NotificationsReducerState = {error: false, loading: false}
        const action = initNotifications("awesome", 5)
        const newState: NotificationsReducerState = action.state;

        expect(
            notificationsReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });

    it('should handle QUERY_NOTIFICATIONS_INIT', () => {

        // should switch to init (success) state
        const state: NotificationsReducerState = {error: false, loading: false}
        const data: NotificationInterface[] = [
            {
                description: "description",
                profile: "profile",
                status: "status",
                time: "time",
                title: "title",
                uuid: "uuid"
            }
        ]
        const action: NotificationsAction = {
            type: ActionTypes.QUERY_NOTIFICATIONS_INIT,
            state: {
                loading: false,
                error: false,
                data,
            }
        }
        const newState: NotificationsReducerState = {...state, ...action.state}

        expect(
            notificationsReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });

    it('should handle QUERY_NOTIFICATIONS_ERROR', () => {

        // should switch to error state
        const state: NotificationsReducerState = {error: false, loading: false}
        const action: NotificationsAction = {
            type: ActionTypes.QUERY_NOTIFICATIONS_ERROR,
            state: {
                loading: false,
                error: true,
            }
        }
        const newState: NotificationsReducerState = action.state;

        expect(
            notificationsReducer(
                state,
                action
            ),
        ).toEqual(newState);
    });
});
