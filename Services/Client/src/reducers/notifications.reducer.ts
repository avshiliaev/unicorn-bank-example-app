import {ActionTypes} from '../constants';
import {
    NotificationInterface,
    NotificationsAction,
    NotificationsReducerState
} from '../interfaces/notification.interface';

const initNotifications = (token: string, count: number): NotificationsAction => {
    return {
        type: ActionTypes.QUERY_NOTIFICATIONS,
        params: {token, count},
        state: {
            loading: true,
            error: false,
            version: 1,
            data: [],
        },
    };
};

const initNotificationsSuccess = (response: NotificationInterface[]): NotificationsAction => {

    const data = response;
    const type = ActionTypes.QUERY_NOTIFICATIONS_SUCCESS;
    return {
        type,
        state: {
            loading: false,
            error: false,
            version: 1,
            data,
        },
    };
}

const initNotificationsError = (): NotificationsAction => {
    return {
        type: ActionTypes.QUERY_NOTIFICATIONS_ERROR,
        state: {
            loading: false,
            error: true,
            version: 1,
            data: [],
        },
    }
};

export {
    initNotifications,
    initNotificationsSuccess,
    initNotificationsError
};

const notificationsInitialState: NotificationsReducerState = {
    loading: false,
    error: false,
    version: 0,
    data: [],
};

const notificationsReducer = (
    state: NotificationsReducerState = notificationsInitialState,
    action: NotificationsAction,
): NotificationsReducerState => {
    switch (action.type) {

        case ActionTypes.QUERY_NOTIFICATIONS:
            return action.state;

        case ActionTypes.QUERY_NOTIFICATIONS_SUCCESS:

            // If the initial data
            if (state.version === 1) {
                action.state.version += state.version;
                return action.state;
            }
            // If an update
            action.state.version += state.version;
            const update = action.state.data;
            const filtered = state.data.filter(function (objFromA) {
                return !update.find(function (objFromB) {
                    return objFromA.id === objFromB.id
                })
            })
            return {...action.state, data: [...filtered, ...update]}

        case ActionTypes.QUERY_NOTIFICATIONS_ERROR:
            return {...state, ...action.state};

        default:
            return state;
    }
};

export default notificationsReducer;
