import { put, takeLatest } from 'redux-saga/effects';
import { ActionTypes } from '../constants';
import { AuthAction, AuthInterface } from '../interfaces/auth.interface';

function* logInSaga(action: AuthAction) {
  const { username } = action.params;
  try {
    const data: AuthInterface = {
      isLoggedIn: true,
      userId: 'wonder',
      username: 'wonder',
    };
    yield localStorage.setItem('username', data.username);
    yield localStorage.setItem('userId', data.userId);
    const actionSuccess: AuthAction = {
      type: ActionTypes.LOG_IN_SUCCESS,
      state: {
        loading: false,
        error: false,
        ...data,
      },
    };
    yield put(actionSuccess);
  } catch (error) {
    const actionError: AuthAction = {
      type: ActionTypes.LOG_IN_ERROR,
      state: {
        loading: false,
        error: true,
        isLoggedIn: false,
      },
    };
    yield put(actionError);
  }
}

export function* logInWatcher() {
  yield takeLatest(ActionTypes.LOG_IN, logInSaga);
}


