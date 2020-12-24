import React, {Fragment} from 'react';
import {connect} from 'react-redux';
import FlexGridDashboard from '../../../components/layout/flex.grid.dashboard';
import AccountsForm from '../../../components/accounts.form';
import {addAccountAsHost} from '../../../reducers/accounts.overview.reducer';
import {AccountInterface} from '../../../interfaces/account.interface';
import DemoPlaceHolder from "../../../components/demo.placeholder";

const DashboardNewRoute = ({windowSize, addAccountAsHost, auth, ...rest}) => {

    const formOnFinish = async (value) => {
        const {profile} = value;
        const addAccountInput: AccountInterface = {profile, status: '', balance: 0, transactions: []};
        await addAccountAsHost(addAccountInput);
    };

    return (
        <Fragment>
            <FlexGridDashboard
                windowSize={windowSize}
                slotOne={<DemoPlaceHolder/>}
                slotTwo={<DemoPlaceHolder/>}
                slotThree={<DemoPlaceHolder/>}
                mainContent={<AccountsForm onFinish={formOnFinish}/>}
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

const mapDispatchToProps = {
    addAccountAsHost,
};

export default connect(mapStateToProps, mapDispatchToProps)(DashboardNewRoute);
