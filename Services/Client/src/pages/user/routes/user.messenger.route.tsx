import React, {Fragment} from "react";
import {connect} from "react-redux";
import {updateViewSettingsAction} from "../../../reducers/view.settings.reducer";
import {NotificationsReducerState} from "../../../interfaces/notification.interface";
import {ViewSettings, ViewSettingsState} from "../../../interfaces/view.settings.interface";
import FlexGridDashboard from "../../../components/layout/flex.grid.dashboard";
import NotificationsList from "../../../components/notifications.list";
import {useAuth0} from "@auth0/auth0-react";
import DemoPlaceHolder from "../../../components/demo.placeholder";

interface UserMessengerProps {
    windowSize: any,
    viewSettings: ViewSettingsState,
    location: any,
    notifications: NotificationsReducerState,
    updateViewSettingsAction: any,
    path: any
}


const UserMessengerRoute = (
    {windowSize, viewSettings, location, notifications, updateViewSettingsAction, ...rest}: UserMessengerProps
) => {

    const {user} = useAuth0();
    const updateCount = (count: number) => {
        const settings: ViewSettings = {notificationsCount: count, currentSender: viewSettings.currentSender};
        updateViewSettingsAction(settings);
    };

    return (
        <Fragment>
            <FlexGridDashboard
                windowSize={windowSize}
                slotOne={<div>{viewSettings.currentSender}</div>}
                slotTwo={<DemoPlaceHolder/>}
                slotThree={<DemoPlaceHolder/>}
                mainContent={
                    <NotificationsList
                        notifications={notifications.data}
                        displayMore={updateCount}
                    />
                }
            />
        </Fragment>
    )
};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        viewSettings: state.viewSettings,
        location: state.router.location,
        notifications: state.notifications,
    };
};

const mapDispatchToProps = {
    updateViewSettingsAction
};

export default connect(
    mapStateToProps,
    mapDispatchToProps,
)(UserMessengerRoute);
