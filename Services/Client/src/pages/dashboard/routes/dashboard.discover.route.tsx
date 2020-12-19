import React, { Fragment } from 'react';
import { connect } from 'react-redux';
import FlexGridDashboard from '../../../components/layout/flex.grid.dashboard';

const DashboardDiscoverRoute = ({ windowSize, ...rest }) => {

  return (
    <Fragment>
      <FlexGridDashboard
        windowSize={windowSize}
        slotOne={<div>Slot One</div>}
        slotTwo={<div>Slot Two</div>}
        slotThree={<div>Slot Three</div>}
        mainContent={<div>AccountsList</div>}
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
