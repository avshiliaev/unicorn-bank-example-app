import {eventChannel} from 'redux-saga';
import {HubConnection} from "@microsoft/signalr";

function createSocketChannel(path: string, token: string, socket: HubConnection, buffer) {

    const subscribe = emitter => {
        const messageHandler = (event) => {
            let action = event.data;
            if (action === null || action === undefined) {
                action = [];
            }
            emitter(action);
        };

        socket.on('Response', messageHandler);

        function unsubscribe() {
            socket.off('Response', messageHandler);
        }

        return unsubscribe;
    }
    return eventChannel(subscribe, buffer);
}

export default createSocketChannel;
