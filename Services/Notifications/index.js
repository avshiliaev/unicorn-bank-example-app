const signalR = require("@microsoft/signalr");

let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/notifications")
    .build();

connection.on("ReceiveMessage", (user, msg) => {
    console.log(user, msg);
});

connection.start()
    .then(() => connection.invoke("SendMessage", "User1", "HELLO"));