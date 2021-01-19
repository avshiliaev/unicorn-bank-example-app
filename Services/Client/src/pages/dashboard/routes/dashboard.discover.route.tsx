import React, {Fragment} from 'react';
import {connect} from 'react-redux';
import FlexGridDashboard from '../../../components/layout/flex.grid.dashboard';
import DemoPlaceHolder from "../../../components/demo.placeholder";


const DashboardDiscoverRoute = ({windowSize, ...rest}) => {

    return (
        <Fragment>
            <FlexGridDashboard
                windowSize={windowSize}
                slotOne={<DemoPlaceHolder/>}
                slotTwo={<DemoPlaceHolder/>}
                slotThree={<DemoPlaceHolder/>}
                mainContent={<DemoPlaceHolder/>}
            />
        </Fragment>
    );
};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        auth: state.auth,
    };
};

export default connect(mapStateToProps)(DashboardDiscoverRoute);
