import React, {Fragment} from 'react';
import {connect} from 'react-redux';
import FlexGridDashboard from '../../../components/layout/flex.grid.dashboard';
import AccountsActions from '../../../components/accounts.actions';
import AccountsList from '../../../components/accounts.list';
import {AccountsOverviewReducerState} from '../../../interfaces/account.interface';
import NotificationsList from '../../../components/notifications.list';
import ProfileStats from '../../../components/profile.stats';
import {NotificationsReducerState} from '../../../interfaces/notification.interface';
import {updateViewSettingsAction} from '../../../reducers/view.settings.reducer';
import {ViewSettings, ViewSettingsState} from '../../../interfaces/view.settings.interface';
import {useAuth0} from "@auth0/auth0-react";

interface DashboardOverviewProps {
    windowSize: any,
    viewSettings: ViewSettingsState,
    accountsOverview: AccountsOverviewReducerState,
    notifications: NotificationsReducerState,
    updateViewSettingsAction: any,
    path: any
    default: any
}

const DashboardOverviewRoute = (
    {
        windowSize,
        viewSettings,
        accountsOverview,
        notifications,
        updateViewSettingsAction,
        ...rest
    }: DashboardOverviewProps) => {

    const {user} = useAuth0();

    const balance = accountsOverview.data.length ? accountsOverview.data
        .map(acc => acc.balance)
        .reduce((a, b) => a + b) : 0;

    const updateCount = (count: number) => {
        const settings: ViewSettings = {
            notificationsCount: count,
            transactionsCount: viewSettings.transactionsCount,
            currentSender: viewSettings.currentSender
        };
        updateViewSettingsAction(settings);
    };

    return (
        <Fragment>
            <FlexGridDashboard
                windowSize={windowSize}
                slotOne={<ProfileStats user={user} balance={balance} windowSize={windowSize}/>}
                slotTwo={<AccountsActions/>}
                slotThree={
                    <NotificationsList
                        loading={notifications.loading}
                        windowSize={windowSize}
                        notifications={notifications.data}
                        displayMore={updateCount}
                    />
                }
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

const mapDispatchToProps = {
    updateViewSettingsAction,
};

export default connect(mapStateToProps, mapDispatchToProps)(DashboardOverviewRoute);
