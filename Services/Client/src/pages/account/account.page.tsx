import React, {useEffect} from 'react';
import {connect} from 'react-redux';
import {Layout} from 'antd';
import SiderBasic from '../../components/layout/sider.basic';
import HeaderBasic from '../../components/layout/header.basic';
import AppLogo from '../../components/logo.icon';
import ProfileIcon from '../../components/profile.icon';
import {getAccount} from '../../reducers/account.reducer';
import AccountSiderMenu from '../../components/account.sider.menu';
import BasicDrawer from '../../components/layout/drawer.basic';
import FooterMobile from '../../components/layout/footer.mobile';
import HeaderMenu from '../../components/header.menu';
import {useAuth0} from "@auth0/auth0-react";

const {Content} = Layout;

const AccountPage = ({windowSize, id, children, getAccount, location, ...rest}) => {

    const {user} = useAuth0();

    useEffect(() => {
        getAccount(id);
    }, []);

    return (
        <Layout style={{minHeight: '100vh'}}>
            <HeaderBasic
                windowSize={windowSize}
                slotLeft={windowSize.large ? <AppLogo/> : <BasicDrawer><AccountSiderMenu/></BasicDrawer>}
                slotMiddle={
                    <HeaderMenu windowSize={windowSize} location={location}/>
                }
                slotRight={<ProfileIcon id={user.sub} size={30}/>}
            />
            <Layout>
                {windowSize.large ? <SiderBasic><AccountSiderMenu/></SiderBasic> : <div/>}
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
        location: state.router.location,
    };
};

const mapDispatchToProps = {
    getAccount,
};

export default connect(mapStateToProps, mapDispatchToProps)(AccountPage);
