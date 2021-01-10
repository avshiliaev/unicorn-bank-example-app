import {eventChannel} from 'redux-saga';
import {HubConnection} from "@microsoft/signalr";
import {apply} from "redux-saga/effects";


function* invokeSocket(socket, method) {
    yield apply(socket, HubConnection.prototype.start, []);
    yield apply(socket, HubConnection.prototype.invoke, [method]);
}

function createSocketChannel(path: string, token: string, socket: HubConnection, buffer) {

    const subscribe = emitter => {
        const messageHandler = (msg) => {
            const data = msg ? msg : []
            emitter(data);
        };

        socket.on('Response', messageHandler);

        function unsubscribe() {
            socket.off('Response', messageHandler);
        }

        return unsubscribe;
    }
    return eventChannel(subscribe, buffer);
}

export {createSocketChannel, invokeSocket};
