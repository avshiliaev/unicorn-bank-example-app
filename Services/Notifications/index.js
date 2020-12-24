const signalR = require("@microsoft/signalr");

const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMkBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImlhdCI6MTYwODU3MjI1NCwiZXhwIjoxNjA4NjU4NjU0LCJhenAiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMiIsInNjb3BlIjoicmVhZDpwcm9maWxlcyByZWFkOm5vdGlmaWNhdGlvbnMgd3JpdGU6YWNjb3VudHMgd3JpdGU6dHJhbnNhY3Rpb24iLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMiLCJwZXJtaXNzaW9ucyI6WyJyZWFkOnByb2ZpbGVzIiwicmVhZDpub3RpZmljYXRpb25zIiwid3JpdGU6YWNjb3VudHMiLCJ3cml0ZTp0cmFuc2FjdGlvbiJdfQ.CqCSd0t41qizxwwzTcnnAALhq00MXrQ45SPphMgIal52dz-fG943HoOARpmRD0-1BRw37ncVrO-33Srne45uZmCkWKgTlkvoJOcYsfP9g9p0aSxX0vozZWmt6z8hSi53QyzrJ3C6oqmJGCaLL7lcZpMoTDlXI3TKd5-vjCfwj6XVmkZxmB-nP8YGs_0YpvKoJ-5Wu9KB3lNNwnDqZW9WdjCGN3T9sAGuGbiGOusAo_kLgDAAjqgxMGzi0C2jTykHge0uJg1PKp9QDyrX--sg2FvtUYzMLzatzqzYBQyPCUxqQznrjGmn47V-BJ-93cWS_7uW2uC2SOrxSVXiXpapiQ"

// https://auth0.com/docs/quickstart/backend/python/02-using
const connection = new signalR.HubConnectionBuilder()
    .withUrl(
        "http://localhost:5000/notifications",
        {
            headers: {authorization: 'Bearer ' + token}
        }
    )
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
        connection.invoke("Request", "wonder")
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

connection.onclose(start);
connection.on("Response", msg => console.log(msg))

// Start the connection.
start();