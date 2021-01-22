const signalR = require("@microsoft/signalr");

const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMkBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImlhdCI6MTYxMTIzNjc1NiwiZXhwIjoxNjExMzIzMTU2LCJhenAiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMiIsInNjb3BlIjoicmVhZDpwcm9maWxlcyByZWFkOm5vdGlmaWNhdGlvbnMgd3JpdGU6YWNjb3VudHMgd3JpdGU6dHJhbnNhY3Rpb25zIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwicGVybWlzc2lvbnMiOlsicmVhZDpwcm9maWxlcyIsInJlYWQ6bm90aWZpY2F0aW9ucyIsIndyaXRlOmFjY291bnRzIiwid3JpdGU6dHJhbnNhY3Rpb25zIl19.pHbv-JpJOqOVI7rkvVUPsXE1SvOS-p3gelgH9VEl0hVf5BEc9H-YqYuiswY-6RTM0FZjhizEf7AAiXjJL8-1SJkSiYAdCP9SUlO7d0oB1vJqwaDpZe-MzZO2z-eI4WT_6gC-mOR9mWq6x7xmbROAsNX6zR-vzv-0hnUseoajEIMUHG9y-7EBfq3UfhCoE1SfyOC2Rs9jQnQ9tmrF6tfeBigmsMfJ2ihlCMDGFXXEWr3cgluD-pYfllscwmzenjrCNwFCrvjQmcoJ5PV8j0yYujPex1qxp11Xi4YOoezDg6G3V1Q8iP_nrF7zOcYdRYMDhF7_DGsi1ZcOWP29cbODtA"

// https://auth0.com/docs/quickstart/backend/python/02-using
// process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';
const connection = new signalR.HubConnectionBuilder()
    .withUrl(
        `http://localhost:5010/profiles?access_token=${token}`,
        {
            // skipNegotiation: true,
            // transport: signalR.HttpTransportType.WebSockets,
        }
    )
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
        connection.invoke("RequestOne", "64c6604f-ed65-43b7-aefb-510afb1effd7", 10)
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

connection.onclose(start);
connection.on("ResponseOne", msg => console.log(msg))
connection.on("error", (error) => console.log(error))

// Start the connection.
start();