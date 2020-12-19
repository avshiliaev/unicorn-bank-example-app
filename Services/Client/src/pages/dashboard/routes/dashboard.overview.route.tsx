import React, { Fragment } from 'react';
import { connect } from 'react-redux';
import FlexGridDashboard from '../../../components/layout/flex.grid.dashboard';
import AccountsActions from '../../../components/accounts.actions';
import AccountsList from '../../../components/accounts.list';
import { AccountsOverviewReducerState } from '../../../interfaces/account.interface';
import NotificationsList from '../../../components/notifications.list';
import ProfileStats from '../../../components/profile.stats';
import { NotificationsReducerState } from '../../../interfaces/notification.interface';
import { updateViewSettingsAction } from '../../../reducers/view.settings.reducer';
import { ViewSettings } from '../../../interfaces/view.settings.interface';

interface DashboardOverviewProps {
  auth: any,
  windowSize: any,
  accountsOverview: AccountsOverviewReducerState,
  notifications: NotificationsReducerState,
  updateViewSettingsAction: any,
  path: any
}

const DashboardOverviewRoute = (
  { auth, windowSize, accountsOverview, notifications, updateViewSettingsAction, ...rest }: DashboardOverviewProps,
) => {

  const balance = accountsOverview.data.length ? accountsOverview.data
    .map(acc => acc.balance)
    .reduce((a, b) => a + b) : 0;

  const updateCount = (count: number) => {
    const settings: ViewSettings = { notificationsCount: count };
    updateViewSettingsAction(settings);
  };

  return (
    <Fragment>
      <FlexGridDashboard
        windowSize={windowSize}
        slotOne={<ProfileStats auth={auth} balance={balance} windowSize={windowSize}/>}
        slotTwo={<AccountsActions/>}
        slotThree={
          <NotificationsList
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
    auth: state.auth,
    accountsOverview: state.accountsOverview,
    notifications: state.notifications,
  };
};

const mapDispatchToProps = {
  updateViewSettingsAction,
};

export default connect(mapStateToProps, mapDispatchToProps)(DashboardOverviewRoute);
