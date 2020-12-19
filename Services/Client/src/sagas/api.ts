import { eventChannel } from 'redux-saga';

interface GenericStreamObject {
  type: string,
  payload?: any[],
}

function createSocketChannel(socket) {

  return eventChannel(emit => {

    const openHandler = () => {
      console.log('connected');
    };
    const messageHandler = (event) => {
      const action: GenericStreamObject = JSON.parse(event.data);
      if (action.payload === null || action.payload === undefined) {
        action.payload = [];
      }
      emit(action);
    };
    const errorHandler = (errorEvent) => {
      emit(new Error(errorEvent.reason));
    };

    socket.onopen = openHandler;
    socket.onmessage = messageHandler;
    socket.onerror = errorHandler;

    return () => {
      socket.off('message', messageHandler);
    };
  });
}

export { createSocketChannel };
