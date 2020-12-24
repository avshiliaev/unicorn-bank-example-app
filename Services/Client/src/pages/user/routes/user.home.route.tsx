import React, {Fragment} from 'react';
import {connect} from 'react-redux';
import {Button, Descriptions} from 'antd';
import FlexGridUser from '../../../components/layout/flex.grid.user';
import BreadCrumbBasic from '../../../components/layout/breadcrumb.basic';
import DemoPlaceHolder from "../../../components/demo.placeholder";
import {useAuth0} from "@auth0/auth0-react";
import { CheckCircleOutlined, CloseCircleOutlined } from '@ant-design/icons';


const UserHomeRoute = ({windowSize, location, ...rest}) => {

    const LogOutButton = ({logOutAction}) => {
        return (
            <Button onClick={() => logOutAction()}>Log Out</Button>
        );
    };

    const {user, logout} = useAuth0();

    const logoutWithRedirect = () =>
        logout({
            returnTo: window.location.origin,
        });

    return (
        <Fragment>
            <FlexGridUser
                windowSize={windowSize}
                breadCrumbs={<BreadCrumbBasic location={location}/>}
                slotOne={
                    <div>
                        <Descriptions title="User Info" layout="horizontal">
                            <Descriptions.Item label="UserName">{user.nickname}</Descriptions.Item>
                            <Descriptions.Item label="Telephone">1810000000</Descriptions.Item>
                            <Descriptions.Item label="Email">{user.email}</Descriptions.Item>
                            <Descriptions.Item label="Verified">
                                {user.email_verified ? <CheckCircleOutlined /> : <CloseCircleOutlined />}
                            </Descriptions.Item>
                            <Descriptions.Item label="Actions">
                                <LogOutButton logOutAction={logoutWithRedirect}/>
                            </Descriptions.Item>
                        </Descriptions>
                    </div>
                }
                slotTwo={<DemoPlaceHolder/>}
            />
        </Fragment>
    );
};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        location: state.router.location,
    };
};

const mapDispatchToProps = {};

export default connect(
    mapStateToProps,
    mapDispatchToProps,
)(UserHomeRoute);
