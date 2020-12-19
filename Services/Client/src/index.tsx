import React from 'react';
import ReactDOM from 'react-dom';
import store, {reachHistory} from './store';
import {Provider} from 'react-redux';
import './index.css';
import {LocationProvider} from '@reach/router';
import App from "./app";

ReactDOM.render(
    <Provider store={store}>
        <LocationProvider history={reachHistory}>
            <App/>
        </LocationProvider>
    </Provider>,
    document.getElementById('root'),
);
