const signalR = require("@microsoft/signalr");

const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw1ZmQ4ZDFlMzIwMTQ4YTAwNjg1MDJkNTYiLCJhdWQiOlsiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImh0dHBzOi8vdW5pY29ybmJhbmsuZXUuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTYxMDA5OTkwMiwiZXhwIjoxNjEwMTg2MzAyLCJhenAiOiJEclFwa2E1azlRdzE5bGdyYVBUb0d6MWhXVWU3N0IxMiIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwiLCJwZXJtaXNzaW9ucyI6WyJyZWFkOm5vdGlmaWNhdGlvbnMiLCJyZWFkOnByb2ZpbGVzIiwid3JpdGU6YWNjb3VudHMiLCJ3cml0ZTp0cmFuc2FjdGlvbnMiXX0.AC30K-VwWpzbK6IG3kgWqLYMDmrbBUJaYxpIIR6tHQ___iYM2ZVP-kvI9FSv1ev1sruTL0se-TtPYIKlO00ZEHLGu1F_v-tLdLppOds4fzf_uo1gJtmYVKe6eXKO56eZKvYlx2rU2kwae9fg-GJccYj31aSSZew3z1LtwTKpuGIYvV9bmEItbkQrfJLd_RyyN4HlFkgxwOBTkqAOVNt_KGfAftUF5-Wk0ZdsaSOzuM-0P_oTGdKOIs8t394ka0Ow9LfCRs3t0cYTyaynbq44czM2QPwYm-DAsV6I4RwAl3fr7xIdd0Cyf1Gqgg6WhHf7JuP__fsIT1DSmO0lQrzm2A"

// https://auth0.com/docs/quickstart/backend/python/02-using
const connection = new signalR.HubConnectionBuilder()
    .withUrl(
        "http://localhost:5010/profiles?access_token=eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw1ZmQ4ZDFlMzIwMTQ4YTAwNjg1MDJkNTYiLCJhdWQiOlsiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImh0dHBzOi8vdW5pY29ybmJhbmsuZXUuYXV0aDAuY29tL3VzZXJpbmZvIl0sImlhdCI6MTYxMDA5OTkwMiwiZXhwIjoxNjEwMTg2MzAyLCJhenAiOiJEclFwa2E1azlRdzE5bGdyYVBUb0d6MWhXVWU3N0IxMiIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwiLCJwZXJtaXNzaW9ucyI6WyJyZWFkOm5vdGlmaWNhdGlvbnMiLCJyZWFkOnByb2ZpbGVzIiwid3JpdGU6YWNjb3VudHMiLCJ3cml0ZTp0cmFuc2FjdGlvbnMiXX0.AC30K-VwWpzbK6IG3kgWqLYMDmrbBUJaYxpIIR6tHQ___iYM2ZVP-kvI9FSv1ev1sruTL0se-TtPYIKlO00ZEHLGu1F_v-tLdLppOds4fzf_uo1gJtmYVKe6eXKO56eZKvYlx2rU2kwae9fg-GJccYj31aSSZew3z1LtwTKpuGIYvV9bmEItbkQrfJLd_RyyN4HlFkgxwOBTkqAOVNt_KGfAftUF5-Wk0ZdsaSOzuM-0P_oTGdKOIs8t394ka0Ow9LfCRs3t0cYTyaynbq44czM2QPwYm-DAsV6I4RwAl3fr7xIdd0Cyf1Gqgg6WhHf7JuP__fsIT1DSmO0lQrzm2A",
        {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
        }
        
    )
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
        connection.invoke("RequestAll")
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

connection.onclose(start);
connection.on("Response", msg => console.log(msg))

// Start the connection.
start();