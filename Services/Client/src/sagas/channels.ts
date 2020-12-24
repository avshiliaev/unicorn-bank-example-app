import {eventChannel} from 'redux-saga';
import {HubConnection} from "@microsoft/signalr";
import {GenericStreamObject} from "../interfaces/stream.interface";
import createClient from "../api/web.socket.api.client";


const createSocketChannel = async (path: string, token: string, method: string) => {

    const socket: HubConnection = createClient(path, token)
    await socket.start()
    await socket.invoke(method)

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
