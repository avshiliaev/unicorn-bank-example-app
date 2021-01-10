import {eventChannel} from 'redux-saga';

const EventEmitter = require('events');


export const socketMock = new EventEmitter();
const createSocketChannelMock = async (path: string, token: string, method: string) => {

    return eventChannel(emitter => {

        const messageHandler = (msg) => {
            const data = msg ? msg : []
            emitter(data);
        };

        socketMock.on("Response", messageHandler);

        return () => {
            socketMock.off('Response', messageHandler);
        };
    });
}

export default createSocketChannelMock;
