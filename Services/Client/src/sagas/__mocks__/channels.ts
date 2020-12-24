import {eventChannel} from 'redux-saga';
import {GenericStreamObject} from "../../interfaces/stream.interface";

const EventEmitter = require('events');


export const socketMock = new EventEmitter();
const createSocketChannelMock = async (path: string, token: string, method: string) => {

    return eventChannel(emit => {

        const messageHandler = (event) => {
            const action: GenericStreamObject = JSON.parse(event.data);
            if (action.payload === null || action.payload === undefined) {
                action.payload = [];
            }
            emit(action);
        };

        socketMock.on("Response", messageHandler);

        return () => {
            socketMock.off('Response', messageHandler);
        };
    });
}

export default createSocketChannelMock;
