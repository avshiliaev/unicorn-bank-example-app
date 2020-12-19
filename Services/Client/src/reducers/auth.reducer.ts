import {ActionTypes} from '../constants';
import {AuthAction, AuthReducerState} from '../interfaces/auth.interface';

const logInAction = (username: string): AuthAction => {
    return {
        type: ActionTypes.LOG_IN,
        params: username,
        state: {
            loading: true,
            isLoggedIn: false,
            error: false,
        },
    };
};

const logOutAction = (): AuthAction => {
    return {
        type: ActionTypes.LOG_OUT,
        state: {
            loading: false,
            isLoggedIn: false,
            error: false,
        },
    };
};


export {logInAction, logOutAction};

const userInitialState: AuthReducerState = !!localStorage.getItem('username')
    ? {
        username: localStorage.getItem('username'),
        isLoggedIn: true,
        userId: localStorage.getItem('userId'),
        loading: false,
        error: false,
    }
    : {
        isLoggedIn: false,
        loading: false,
        error: false,
    };

const authReducer = (
    state: AuthReducerState = userInitialState,
    action: AuthAction,
): AuthReducerState => {
    switch (action.type) {
        case ActionTypes.LOG_IN:
            return action.state;
        case ActionTypes.LOG_IN_SUCCESS:
            return action.state;
        case ActionTypes.LOG_IN_ERROR:
            return action.state;
        default:
            return state;
    }
};

export default authReducer;
