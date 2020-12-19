import {UserAction, UserReducerState} from '../interfaces/user.interface';
import {ActionTypes} from '../constants';

const getUser = (userId: string): UserAction => {
    return {
        type: ActionTypes.GET_USER,
        params: userId,
        state: {
            loading: true,
            error: false,
        },
    };
};

export {getUser};

const userReducer = (state: UserReducerState, action: UserAction): UserReducerState => {
    switch (action.type) {
        case ActionTypes.GET_USER:
            return {...state, ...action.state};
        case ActionTypes.GET_USER_SUCCESS:
            return {...state, ...action.state};
        case ActionTypes.GET_USER_ERROR:
            return {...state, ...action.state};
        default:
            return {...state};
    }
};

export default userReducer;
