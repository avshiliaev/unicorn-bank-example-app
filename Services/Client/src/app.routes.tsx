import {Redirect, Router} from '@reach/router';
import React from 'react';
import DashboardPage from './pages/dashboard/dashboard.page';
import AccountPage from './pages/account/account.page';
import UserPage from './pages/user/user.page';
import AccountHomeRoute from './pages/account/routes/account.home.route';
import UserHomeRoute from './pages/user/routes/user.home.route';
import DashboardOverviewRoute from './pages/dashboard/routes/dashboard.overview.route';
import DashboardDiscoverRoute from './pages/dashboard/routes/dashboard.discover.route';
import DashboardNewRoute from './pages/dashboard/routes/dashboard.new.route';
import UserMessengerRoute from "./pages/user/routes/user.messenger.route";
import AccountTransactionsRoute from "./pages/account/routes/account.transactions.route";

const AppRoutes = () => {

    return (
        <Router>
            <Redirect noThrow from="/" to="dashboard/home"/>
            <DashboardPage path="dashboard">
                <DashboardOverviewRoute default path="home"/>
                <DashboardDiscoverRoute path="discover"/>
                <DashboardNewRoute path="new"/>
            </DashboardPage>
            <AccountPage path="account/:id">
                <AccountHomeRoute path="home"/>
                <AccountTransactionsRoute path="transactions"/>
            </AccountPage>
            <UserPage path="user/:id">
                <UserHomeRoute path="home"/>
                <UserMessengerRoute path="messages"/>
            </UserPage>
        </Router>
    );
};

export default AppRoutes;
