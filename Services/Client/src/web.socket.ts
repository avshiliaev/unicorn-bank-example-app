import * as signalR from "@microsoft/signalr";
import {HttpTransportType} from "@microsoft/signalr";

const createWebSocketConnection = (path: string) => {
    const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMkBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImlhdCI6MTYwODgxNTU2NSwiZXhwIjoxNjA4OTAxOTY1LCJhenAiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMiIsInNjb3BlIjoicmVhZDpwcm9maWxlcyByZWFkOm5vdGlmaWNhdGlvbnMgd3JpdGU6YWNjb3VudHMgd3JpdGU6dHJhbnNhY3Rpb24iLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMiLCJwZXJtaXNzaW9ucyI6WyJyZWFkOnByb2ZpbGVzIiwicmVhZDpub3RpZmljYXRpb25zIiwid3JpdGU6YWNjb3VudHMiLCJ3cml0ZTp0cmFuc2FjdGlvbiJdfQ.jz2ays1jY9BK1db9snYM96WDc7r9MXlVntefocml_QrnH7gAs1zkwbdkgLNiiGcCf1qPBvRgqO66cpMVY9eLnJL-8fzDNn5AeNKjHsco1bTa_iVumPmIEugEm2mglh9DT6bjaW9DXWr5EjPNLDtZWra13XvN1ESsaJNm_qjPa94bJazi-ABFx0_TVOvjKJsLyq91zwWEv5a6IrQMLqFWUljxwZaCd1RefGJrXeB2ct-vVE_2swYRs4Y_sUr92tmJhwG14lF5DWrnkoPIp4qG1629PgZ35o-CcNUEVjPqpF9jTlXE95tnUuXGpTICMFYSSXQV1kOY4W3EmRidTXgMsg"
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
