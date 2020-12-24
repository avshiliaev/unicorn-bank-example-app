import {Redirect, Router} from '@reach/router';
import React from 'react';
import DashboardPage from './pages/dashboard/dashboard.page';
import ProjectPage from './pages/account/account.page';
import UserPage from './pages/user/user.page';
import ProjectHomeRoute from './pages/account/routes/account.home.route';
import UserHomeRoute from './pages/user/routes/user.home.route';
import DashboardOverviewRoute from './pages/dashboard/routes/dashboard.overview.route';
import DashboardDiscoverRoute from './pages/dashboard/routes/dashboard.discover.route';
import DashboardNewRoute from './pages/dashboard/routes/dashboard.new.route';

const AppRoutes = () => {

    return (
        <Router>
            <Redirect noThrow from="/" to="dashboard/home"/>
            <DashboardPage path="dashboard">
                <DashboardOverviewRoute default path="home"/>
                <DashboardDiscoverRoute path="discover"/>
                <DashboardNewRoute path="new"/>
            </DashboardPage>
            <ProjectPage path="account/:id">
                <ProjectHomeRoute path="home"/>
            </ProjectPage>
            <UserPage path="user/:id">
                <UserHomeRoute path="home"/>
            </UserPage>
        </Router>
    );
};

export default AppRoutes;
