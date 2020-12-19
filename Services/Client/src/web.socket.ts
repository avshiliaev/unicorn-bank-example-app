const createWebSocketConnection = (path: string) => {
    const wsUrl = 'ws://localhost:8082/streams';
    const url = `${wsUrl}${path}`
    return new WebSocket(url);
};

export default createWebSocketConnection;
