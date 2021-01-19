import * as signalR from "@microsoft/signalr";
import {HttpTransportType} from "@microsoft/signalr";
import {Environment} from "../constants/environment";

const createClient = (path: string, token: string) => {

    const wsUrl: string = Environment.PATHS_WS;
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

export default createClient;
