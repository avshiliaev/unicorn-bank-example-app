import * as signalR from "@microsoft/signalr";
import {HttpTransportType} from "@microsoft/signalr";

const createClient = (path: string, token: string) => {

    const wsUrl = process.env.REACT_APP_PATHS_CROSS_ORIGIN ?? "";
    const url = `${wsUrl}${path}?access_token=${token}`

    return new signalR.HubConnectionBuilder()
        .withUrl(
            url,
            {
                // skipNegotiation: true,
                // transport: HttpTransportType.WebSockets,
            }
        )
        .configureLogging(signalR.LogLevel.Information)
        .build();
};

export default createClient;
