import {getAccountDetailSaga} from "../account.detail.sagas";
import {getAccount} from "../../reducers/account.reducer";
import {AccountAction} from "../../interfaces/account.interface";
import {call, put, take} from "redux-saga/effects";
import createWebSocketConnection from "../../web.socket";
import {createSocketChannel} from "../api";
import {ActionTypes} from "../../constants";


//TODO https://redux-saga.js.org/docs/advanced/Testing.html
describe('getAccountDetailSaga tests', () => {
    it('should ...', () => {

        const actionInit: AccountAction = getAccount("awesome");
        const {params} = actionInit;
        const path = `/profiles?account=${params}`;

        const saga = getAccountDetailSaga(actionInit);

        expect(saga.next().value).toStrictEqual(call(createWebSocketConnection, path));
        expect(saga.next().value).toStrictEqual(call(createSocketChannel, undefined));

        expect(saga.next().value).toStrictEqual(take());

        const actionError: AccountAction = {
            type: ActionTypes.GET_ACCOUNT_DETAIL_ERROR,
            state: {
                loading: false,
                error: true,
            }
        }
        expect(saga.next().value).toStrictEqual(put(actionError));
        expect(saga.next().done).toEqual(true);

    });

});
