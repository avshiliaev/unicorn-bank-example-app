import * as signalR from "@microsoft/signalr";
import {HttpTransportType} from "@microsoft/signalr";

const createWebSocketConnection = (path: string, token: string) => {

    const wsUrl = 'http://localhost:5000';
    const url = `${wsUrl}${path}?access_token=${token}`

    return new signalR.HubConnectionBuilder()
        .withUrl(
            url,
            {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
            }
        )
        .configureLogging(signalR.LogLevel.Information)
        .build();
};

export default createWebSocketConnection;
