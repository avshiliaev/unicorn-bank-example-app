import React from 'react';
import {connect} from 'react-redux';
import Login from './login';
import AppRoutes from './app.routes';
import {useAuth0} from "@auth0/auth0-react";

const App = () => {

    const {user, isAuthenticated} = useAuth0();

    return isAuthenticated && user
        ? <AppRoutes/>
        : <Login/>;
};

const mapStateToProps = (state) => {
    return {};
};

export default connect(mapStateToProps)(App);
