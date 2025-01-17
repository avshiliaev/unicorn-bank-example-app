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
import {Auth0Provider} from "@auth0/auth0-react";
import {Environment} from "./constants/environment";


const {createReduxHistory, routerMiddleware} = createReduxHistoryContext({
    history: createBrowserHistory(),
});
const store = configureStore(routerMiddleware);
const reachHistory = reachify(createReduxHistory(store));

store.runSaga(rootSaga)

ReactDOM.render(
    <Provider store={store}>
        <LocationProvider history={reachHistory}>
            <Auth0Provider
                domain={Environment.AUTH_DOMAIN}
                clientId={Environment.AUTH_CLIENT_ID}
                audience={Environment.AUTH_AUDIENCE}
                redirectUri={window.location.origin}
            >
                <App/>
            </Auth0Provider>
        </LocationProvider>
    </Provider>,
    document.getElementById('root'),
);


