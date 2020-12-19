import React from 'react';
import { connect } from 'react-redux';
import { Layout } from 'antd';
import SiderBasic from '../../components/layout/sider.basic';
import HeaderBasic from '../../components/layout/header.basic';
import AppLogo from '../../components/logo.icon';
import HeaderMenu from '../../components/header.menu';
import ProfileIcon from '../../components/profile.icon';
import UserSiderMenu from '../../components/user.sider.menu';
import { getUser } from '../../reducers/user.reducer';
import BasicDrawer from '../../components/layout/drawer.basic';
import FooterMobile from '../../components/layout/footer.mobile';

const { Content } = Layout;

const UserPage = ({ windowSize, auth, getUser, children, location, id, ...rest }) => {

  // getUser(id);

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <HeaderBasic
        windowSize={windowSize}
        slotLeft={windowSize.large ? <AppLogo/> : <BasicDrawer><UserSiderMenu/></BasicDrawer>}
        slotMiddle={
          <HeaderMenu windowSize={windowSize} location={location}/>
        }
        slotRight={<ProfileIcon id={auth.userId} size={30}/>}
      />
      <Layout>
        {windowSize.large ? <SiderBasic><UserSiderMenu/></SiderBasic> : <div/>}
        <Content style={{ padding: windowSize.large ? 16 : 0 }}>
          {children}
        </Content>
        {!windowSize.large && <FooterMobile auth={auth} location={location}/>}
      </Layout>
    </Layout>
  );
};

const mapStateToProps = (state) => {
  return {
    windowSize: state.windowSize.greaterThan,
    auth: state.auth,
    location: state.router.location,
  };
};

const mapDispatchToProps = {
  getUser,
};

export default connect(mapStateToProps, mapDispatchToProps)(UserPage);
