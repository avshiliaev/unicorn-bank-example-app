import React, {Fragment} from 'react';
import {connect} from 'react-redux';
import FlexGridDashboard from '../../../components/layout/flex.grid.dashboard';
import AccountsForm from '../../../components/accounts.form';
import {addAccountAsHost} from '../../../reducers/accounts.overview.reducer';
import {AccountInterface} from '../../../interfaces/account.interface';

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
                slotOne={<div>Slot One</div>}
                slotTwo={<div>Slot Two</div>}
                slotThree={<div>Slot Three</div>}
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
