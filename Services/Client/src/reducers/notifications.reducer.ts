import {ActionTypes} from '../constants';
import {NotificationsAction, NotificationsReducerState} from '../interfaces/notification.interface';

const initNotifications = (userId: string, count: number): NotificationsAction => {
    return {
        type: ActionTypes.QUERY_NOTIFICATIONS,
        params: {userId, count},
        state: {
            loading: true,
            error: false,
            data: [],
        },
    };
};

export {initNotifications};

const notificationsInitialState: NotificationsReducerState = {
    loading: false,
    error: false,
    data: [],
};

const notificationsReducer = (
    state: NotificationsReducerState = notificationsInitialState,
    action: NotificationsAction,
): NotificationsReducerState => {
    switch (action.type) {

        case ActionTypes.QUERY_NOTIFICATIONS:
            return {...state};

        case ActionTypes.QUERY_NOTIFICATIONS_INIT:
            // TODO the array gets overwritten!
            return {...state, ...action.state};

        case ActionTypes.QUERY_NOTIFICATIONS_UPDATE:
            const update = action.state.data[0];

            // Update or add new one
            if (state.data.filter(a => a.uuid === update.uuid).length > 0) {
                const data = state.data.map(account => {
                    if (account.uuid === update.uuid) {
                        return {...account, ...update};
                    }
                    return account;
                });
                return {...state, data};
            } else {
                const data = [...state.data, update];
                return {...state, data};
            }
        case ActionTypes.QUERY_NOTIFICATIONS_ERROR:
            return {...state, ...action.state};

        case ActionTypes.ADD_ACCOUNT:
            return {...state, data: [...state.data, ...action.state.data]};

        default:
            return state;
    }
};

export default notificationsReducer;
