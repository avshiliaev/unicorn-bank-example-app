import {eventChannel} from 'redux-saga';
import {HubConnection} from "@microsoft/signalr";
import {apply} from "redux-saga/effects";
import {Buffer} from "@redux-saga/types";

export interface SocketChannelProps {
    path: string
    token: string
    socket: HubConnection
    responseMethod: string
    buffer: Buffer<any>
}


function* invokeSocket(socket, invokeArgs) {
    yield apply(socket, HubConnection.prototype.start, []);
    yield apply(socket, HubConnection.prototype.invoke, invokeArgs);
}

function createSocketChannel(props: SocketChannelProps) {

    const subscribe = emitter => {
        const messageHandler = (msg) => {
            const data = msg ? msg : []
            emitter(data);
        };

        props.socket.on(props.responseMethod, messageHandler);

        function unsubscribe() {
            props.socket.off(props.responseMethod, messageHandler);
        }

        return unsubscribe;
    }
    return eventChannel(subscribe, props.buffer);
}

export {createSocketChannel, invokeSocket};
