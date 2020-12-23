import {all} from 'redux-saga/effects';
import {getAccountsWatcher} from './accounts.overview.sagas';
import {getAccountDetailWatcher} from './account.detail.sagas';
import {getUserWatcher} from './user.sagas';
import {getNotificationsWatcher} from './notifications.sagas';

export default function* rootSaga() {
    yield all([
        getNotificationsWatcher(),
        getAccountsWatcher(),
        getAccountDetailWatcher(),
        getUserWatcher(),
    ]);
}
