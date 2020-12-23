import React, {Fragment} from 'react';
import {connect} from 'react-redux';
import {Button} from 'antd';
import FlexGridUser from '../../../components/layout/flex.grid.user';
import BreadCrumbBasic from '../../../components/layout/breadcrumb.basic';
import {UserInterface} from '../../../interfaces/user.interface';
import DemoPlaceHolder from "../../../components/demo.placeholder";

const LogOutButton = ({logOutAction}) => {
    return (
        <Button onClick={() => logOutAction()}>Log Out</Button>
    );
};

const MyProfile = ({windowSize, theUser, logOutAction, location}) => {

    return (
        <Fragment>
            <FlexGridUser
                windowSize={windowSize}
                breadCrumbs={<BreadCrumbBasic location={location}/>}
                slotOne={<div>
                    <LogOutButton logOutAction={logOutAction}/>
                </div>}
                slotTwo={<DemoPlaceHolder/>}
            />
        </Fragment>
    );
};

const OtherProfile = ({windowSize, theUser, location}) => {

    return (
        <Fragment>
            <FlexGridUser
                windowSize={windowSize}
                breadCrumbs={<BreadCrumbBasic location={location}/>}
                slotOne={<div>This is NOT me</div>}
                slotTwo={<div>{theUser.username}</div>}
            />
        </Fragment>
    );
};

const UserHomeRoute = ({windowSize, logOutAction, location, auth, user, ...rest}) => {

    const theUser: UserInterface = user;

    return theUser.id !== undefined
        ? (
            theUser.id === auth.userId
                ? <MyProfile
                    location={location}
                    windowSize={windowSize}
                    theUser={theUser}
                    logOutAction={logOutAction}
                />
                : <OtherProfile windowSize={windowSize} theUser={theUser} location={location}/>
        )
        : <div/>;
};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        auth: state.auth,
        user: state.user,
        location: state.router.location,
    };
};

const mapDispatchToProps = {

};

export default connect(
    mapStateToProps,
    mapDispatchToProps,
)(UserHomeRoute);
