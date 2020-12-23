import * as signalR from "@microsoft/signalr";
import {HttpTransportType} from "@microsoft/signalr";

const createWebSocketConnection = (path: string) => {
    const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMkBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImlhdCI6MTYwODYyNzY5NiwiZXhwIjoxNjA4NzE0MDk2LCJhenAiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMiIsInNjb3BlIjoicmVhZDpwcm9maWxlcyByZWFkOm5vdGlmaWNhdGlvbnMgd3JpdGU6YWNjb3VudHMgd3JpdGU6dHJhbnNhY3Rpb24iLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMiLCJwZXJtaXNzaW9ucyI6WyJyZWFkOnByb2ZpbGVzIiwicmVhZDpub3RpZmljYXRpb25zIiwid3JpdGU6YWNjb3VudHMiLCJ3cml0ZTp0cmFuc2FjdGlvbiJdfQ.Pn5QfVlDDbw2TwcZbZ9xNJBM13SVW6SRrHGMCV9gzaAl1C8782TCsw2Zyh3eKT5AWjbM2Z7KFj8GhIVNL20NMkRaiuACbT992Rk12biX1I31Bhi15Vbz1VoY1Jrgub3JFBcAIgC4hpxx3BGP1nNtj0fl6LzuyLUJNBS4nvw5bktw6sZcezs-7GjpV2jpyQnGot9HaYjx0XeOWbN8atFHR3Va3amRIbVMsNh2HRHbi8edscOsR_E8IFG3lQWy-1wHKabhfpapTAJU18VYiT4t_LtbZn8oitmjamASPYd59joN-jRP8UsoNjanA_lZU9d_wB6t2OLzJPO6wnWvmA8oMQ"
    const wsUrl = 'http://localhost:5000';
    const url = `${wsUrl}${path}?access_token=${token}`

    // https://auth0.com/docs/quickstart/backend/python/02-using
    return new signalR.HubConnectionBuilder()
        .withUrl(
            url,
            {
                // headers: {authorization: 'Bearer ' + token},
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
            }
        )
        .configureLogging(signalR.LogLevel.Information)
        .build();
};

export default createWebSocketConnection;
