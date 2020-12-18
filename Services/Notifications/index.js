const signalR = require("@microsoft/signalr");

const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMkBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImlhdCI6MTYwODMwMzMwNiwiZXhwIjoxNjA4Mzg5NzA2LCJhenAiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMiIsInNjb3BlIjoicmVhZDpwcm9maWxlcyByZWFkOm5vdGlmaWNhdGlvbnMgd3JpdGU6YWNjb3VudHMgd3JpdGU6dHJhbnNhY3Rpb24iLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMiLCJwZXJtaXNzaW9ucyI6WyJyZWFkOnByb2ZpbGVzIiwicmVhZDpub3RpZmljYXRpb25zIiwid3JpdGU6YWNjb3VudHMiLCJ3cml0ZTp0cmFuc2FjdGlvbiJdfQ.oWvBK2LPH8oY_aXDmUZwCboahVcd2L_l1srMlljc9ySU87Sel0Zba0yuKQ6_wjY7GS8LZTTY7Z1w4jsV5eY4Qe8sfb1LBLlsmEGF9VkauHGcp6VkdEFFUB2nozSK4GWkAWEkke4uvpV9z-xr1DuYnyQB6G1iWquESf7XDOgXaQRXx1QrAGzAkK2SHF2_RQmfbqSTkSNXmjFFKRyghg5ZN8ObOf22VX7cekraU8JiSEiW4Pg99P_fOSs4OZxJZiYvGR6h-qMEVrIWRagWyVLW_WHytfchmo_CuQ0GPqAXM0UwFIFBI9F2nFKOxT5tDDLxpUWeKi0yUEVVxM55BaQ4Ow"

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