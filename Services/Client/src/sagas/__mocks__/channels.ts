import {eventChannel} from 'redux-saga';

const EventEmitter = require('events');

class MyEmitter extends EventEmitter {
}

export interface GenericStreamObject {
    type: string,
    payload?: any[],
}

export const mockSocket = new MyEmitter();
const createSocketChannel = async (path: string, method: string, id: string) => {

    return eventChannel(emit => {

        const messageHandler = (event) => {
            const action: GenericStreamObject = JSON.parse(event.data);
            if (action.payload === null || action.payload === undefined) {
                action.payload = [];
            }
            emit(action);
        };

        mockSocket.on("Response", messageHandler);

        return () => {
            mockSocket.off('Response', messageHandler);
        };
    });
}

// @ts-ignore
export {createSocketChannel};
