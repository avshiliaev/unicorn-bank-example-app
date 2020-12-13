const signalR = require("@microsoft/signalr");

let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/notifications")
    .build();

connection.on("Response", (msg) => {
    console.log(msg);
});

connection.start()
    .then(() => connection.invoke("Request", "User1"));