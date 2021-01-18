const signalR = require("@microsoft/signalr");

const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ilc1MFBrT09KUHV6dWJUcXVkSnNvdyJ9.eyJpc3MiOiJodHRwczovL3VuaWNvcm5iYW5rLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMkBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly91bmljb3JuYmFuay5pbyIsImlhdCI6MTYxMDk1NjEwNSwiZXhwIjoxNjExMDQyNTA1LCJhenAiOiJvRFJOR3d3SDk1elpNVU95ZDF2VG9Na1hHRXlnbzRHMiIsInNjb3BlIjoicmVhZDpwcm9maWxlcyByZWFkOm5vdGlmaWNhdGlvbnMgd3JpdGU6YWNjb3VudHMgd3JpdGU6dHJhbnNhY3Rpb25zIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwicGVybWlzc2lvbnMiOlsicmVhZDpwcm9maWxlcyIsInJlYWQ6bm90aWZpY2F0aW9ucyIsIndyaXRlOmFjY291bnRzIiwid3JpdGU6dHJhbnNhY3Rpb25zIl19.ZhcVlNs2H21JyBglX7ZTtU5dsp3_yr86bWNRoPIiDMDV9FA6oGfaX_bBB9JmgKEKj3BO-mgtv4ooaaA-nHARc2BGpLPU_s3eaThUJPTqubnXHzLHoUpSGoMcyYg1PgVWvSgzZAyCcHC2975dCgxroEeppSwY7B58fm_-PyLDE8wVY_FBeQOWrh74oeowkfbqYeQ_wXXkaJkPm0lM3-0i0O_cVOJH1uZ60bf-szPW78IKb-OykHy53epsyr8d4ILgli0vbwthp-49upqAjEwPdmRnFWx2lp66T_vx9XRi9DUv19l0I0FnbEGQdwnb3HS2gx4c_WwdKfg1DAuJDsc6rw"

// https://auth0.com/docs/quickstart/backend/python/02-using
// process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';
const connection = new signalR.HubConnectionBuilder()
    .withUrl(
        `http://localhost:80/api/profiles?access_token=${token}`,
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
        connection.invoke("RequestAll")
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

connection.onclose(start);
connection.on("ResponseAll", msg => console.log(msg))
connection.on("error", (error) => console.log(error))

// Start the connection.
start();