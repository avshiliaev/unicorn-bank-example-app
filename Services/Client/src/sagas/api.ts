import {eventChannel} from 'redux-saga';
import * as signalR from "@microsoft/signalr";

interface GenericStreamObject {
    type: string,
    payload?: any[],
}

function createSocketChannel(socket: signalR.HubConnection) {

    return eventChannel(emit => {

        const messageHandler = (event) => {
            const action: GenericStreamObject = JSON.parse(event.data);
            if (action.payload === null || action.payload === undefined) {
                action.payload = [];
            }
            emit(action);
        };

        socket.on("Response", messageHandler);

        return () => {
            socket.off('Response', messageHandler);
        };
    });
}

export {createSocketChannel};
