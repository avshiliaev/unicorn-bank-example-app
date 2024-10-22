import React, {Fragment} from 'react';
import {connect} from 'react-redux';
import FlexGridDashboard from '../../../components/layout/flex.grid.dashboard';
import AccountsActions from '../../../components/accounts.actions';
import AccountsList from '../../../components/accounts.list';
import {AccountsOverviewReducerState} from '../../../interfaces/account.interface';
import ProfileStats from '../../../components/profile.stats';
import {NotificationsReducerState} from '../../../interfaces/notification.interface';
import {ViewSettingsState} from '../../../interfaces/view.settings.interface';
import {useAuth0} from "@auth0/auth0-react";
import DemoPlaceHolder from "../../../components/demo.placeholder";
import {Environment} from "../../../constants/environment";

interface DashboardOverviewProps {
    windowSize: any,
    viewSettings: ViewSettingsState,
    accountsOverview: AccountsOverviewReducerState,
    notifications: NotificationsReducerState,
    path: any
    default: any
}

const DashboardOverviewRoute = (
    {
        windowSize,
        viewSettings,
        accountsOverview,
        notifications,
        ...rest
    }: DashboardOverviewProps) => {

    const {user, getAccessTokenSilently} = useAuth0();

    const beUrl: string = Environment.PATHS_WS;
    const url = `${beUrl}/api/accounts`

    const createAccount = async () => {
        const token = await getAccessTokenSilently();
        const response = await fetch(url, {
            method: 'get',
            headers: new Headers({
                'Authorization': 'Bearer ' + token,
                'Content-Type': 'application/json'
            })
        });
        console.log(response);
    };

    const balance = accountsOverview.data.length ? accountsOverview.data
        .map(acc => acc.balance)
        .reduce((a, b) => a + b) : 0;

    return (
        <Fragment>
            <FlexGridDashboard
                windowSize={windowSize}
                slotOne={<ProfileStats user={user} balance={balance} windowSize={windowSize}/>}
                slotTwo={<AccountsActions handleClick={createAccount}/>}
                slotThree={<DemoPlaceHolder/>}
                mainContent={
                    <AccountsList
                        accounts={accountsOverview.data}
                        windowSize={windowSize}
                        loading={accountsOverview.loading}
                    />
                }
            />
        </Fragment>
    );

};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        viewSettings: state.viewSettings,
        accountsOverview: state.accountsOverview,
        notifications: state.notifications,
    };
};

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(DashboardOverviewRoute);
