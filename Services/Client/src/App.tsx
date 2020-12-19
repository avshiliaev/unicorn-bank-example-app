import React from 'react';
import { connect } from 'react-redux';
import Login from './Login';
import AppRoutes from './app.routes';

const App = ({ auth }) => {

  return auth.isLoggedIn
    ? <AppRoutes/>
    : <Login/>;
};

const mapStateToProps = (state) => {
  return {
    auth: state.auth,
  };
};

export default connect(mapStateToProps)(App);
