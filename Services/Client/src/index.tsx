import React from 'react';
import ReactDOM from 'react-dom';
import store, { reachHistory } from './store';
import { Provider } from 'react-redux';
import './index.css';
import App from './App';
import { LocationProvider } from '@reach/router';

ReactDOM.render(
  <Provider store={store}>
    <LocationProvider history={reachHistory}>
      <App/>
    </LocationProvider>
  </Provider>,
  document.getElementById('root'),
);
