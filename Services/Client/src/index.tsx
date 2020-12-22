import React from 'react';
import ReactDOM from 'react-dom';
import {Provider} from 'react-redux';
import './index.css';
import {LocationProvider} from '@reach/router';
import App from "./app";
import rootSaga from "./sagas";
import {createReduxHistoryContext, reachify} from "redux-first-history";
import {createBrowserHistory} from "history";
import configureStore from "./store";


const {createReduxHistory, routerMiddleware} = createReduxHistoryContext({
    history: createBrowserHistory(),
});
const store = configureStore(routerMiddleware);
const reachHistory = reachify(createReduxHistory(store));

store.runSaga(rootSaga)

ReactDOM.render(
    <Provider store={store}>
        <LocationProvider history={reachHistory}>
            <App/>
        </LocationProvider>
    </Provider>,
    document.getElementById('root'),
);
