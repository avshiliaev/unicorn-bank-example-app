import {combineReducers} from 'redux';
import accountsOverviewReducer from './accounts.overview.reducer';
import accountReducer from './account.reducer';
import userReducer from './user.reducer';
import {createResponsiveStateReducer} from 'redux-responsive';
import {createReduxHistoryContext} from 'redux-first-history';
import {createBrowserHistory} from 'history';
import notificationsReducer from './notifications.reducer';
import viewSettingsReducer from './view.settings.reducer';

const {routerReducer} = createReduxHistoryContext({
    history: createBrowserHistory(),
});

const breakPoints = {
    extraSmall: 480,
    small: 576,
    medium: 768,
    large: 992,
    extraLarge: 1200,
};

const rootReducer = combineReducers({
    viewSettings: viewSettingsReducer,
    notifications: notificationsReducer,
    accountsOverview: accountsOverviewReducer,
    account: accountReducer,
    user: userReducer,
    router: routerReducer,
    windowSize: createResponsiveStateReducer(breakPoints),
});

export default rootReducer;
