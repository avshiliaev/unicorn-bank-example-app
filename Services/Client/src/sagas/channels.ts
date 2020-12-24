import {eventChannel} from 'redux-saga';
import {HubConnection} from "@microsoft/signalr";
import createWebSocketConnection from "../web.socket";
import {GenericStreamObject} from "../interfaces/stream.interface";


const createSocketChannel = async (path: string, method: string) => {

    const socket: HubConnection = createWebSocketConnection(path)
    await socket.start()
    await socket.invoke(method, {})

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

export default createSocketChannel;
