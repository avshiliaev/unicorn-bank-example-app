import {ViewSettingsAction, ViewSettingsState} from '../interfaces/view.settings.interface';
import {ActionTypes} from "../constants/action.types";

const updateNotificationsCountAction = (token, count): ViewSettingsAction => {
    return {
        type: ActionTypes.UPDATE_VIEW_SETTINGS_NOTIFICATIONS_COUNT,
        params: {token, count},
        state: {
            loading: false,
            error: false,
            notificationsCount: count,
        },
    };
};

export {updateNotificationsCountAction};

const viewSettingsInitState: ViewSettingsState = {
    loading: false,
    error: false,
    notificationsCount: 10,
    transactionsCount: 10,
    currentSender: "Notifications"
};

const viewSettingsReducer = (
    state: ViewSettingsState = viewSettingsInitState,
    action: ViewSettingsAction,
): ViewSettingsState => {
    switch (action.type) {
        case ActionTypes.UPDATE_VIEW_SETTINGS_NOTIFICATIONS_COUNT:
            return {...state, ...action.state};
        default:
            return {...state};
    }
};

export default viewSettingsReducer;
