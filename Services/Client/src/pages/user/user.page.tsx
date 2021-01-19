import React, {useEffect} from 'react';
import {connect} from 'react-redux';
import {Layout} from 'antd';
import SiderBasic from '../../components/layout/sider.basic';
import HeaderBasic from '../../components/layout/header.basic';
import AppLogo from '../../components/logo.icon';
import HeaderMenu from '../../components/header.menu';
import ProfileIcon from '../../components/profile.icon';
import UserSiderMenu from '../../components/user.sider.menu';
import BasicDrawer from '../../components/layout/drawer.basic';
import FooterMobile from '../../components/layout/footer.mobile';
import {useAuth0} from "@auth0/auth0-react";
import {ViewSettingsState} from "../../interfaces/view.settings.interface";
import {initNotifications} from "../../reducers/notifications.reducer";

const {Content} = Layout;


interface UserPageProps {
    windowSize: any
    viewSettings: ViewSettingsState
    accountsLoaded: number
    notificationsLoaded: number
    initNotifications: any
    children: any
    location: any
    path: any
}

const UserPage = (
    {
        windowSize,
        viewSettings,
        accountsLoaded,
        notificationsLoaded,
        initNotifications,
        children,
        location,
        ...rest
    }: UserPageProps) => {

    const {user, getAccessTokenSilently, getAccessTokenWithPopup} = useAuth0();

    useEffect(() => {
        getAccessTokenSilently().then(token => {
            !notificationsLoaded && initNotifications(token, viewSettings.notificationsCount);
        });
    }, []);

    return (
        <Layout style={{minHeight: '100vh'}}>
            <HeaderBasic
                windowSize={windowSize}
                slotLeft={
                    windowSize.large ?
                        <AppLogo/>
                        : <BasicDrawer>
                            <UserSiderMenu location={location} windowSize={windowSize}/>
                        </BasicDrawer>
                }
                slotMiddle={
                    <HeaderMenu windowSize={windowSize} location={location}/>
                }
                slotRight={
                    <ProfileIcon id={user.sub} size={30} image={user.picture}/>
                }
            />
            <Layout>
                {
                    windowSize.large ?
                        <SiderBasic>
                            <UserSiderMenu location={location} windowSize={windowSize}/>
                        </SiderBasic>
                        : <div/>
                }
                <Content style={{padding: windowSize.large ? 16 : 0}}>
                    {children}
                </Content>
                {!windowSize.large && <FooterMobile auth={user} location={location}/>}
            </Layout>
        </Layout>
    );
};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        viewSettings: state.viewSettings,
        location: state.router.location,
        accountsLoaded: state.accountsOverview.data.length,
        notificationsLoaded: state.notifications.data.length,
    };
};

const mapDispatchToProps = {
    initNotifications
};

export default connect(mapStateToProps, mapDispatchToProps)(UserPage);
