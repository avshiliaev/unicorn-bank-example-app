import React from 'react';
import {connect} from 'react-redux';
import FlexGridAccount from '../../../components/layout/flex.grid.account';
import BreadCrumbBasic from '../../../components/layout/breadcrumb.basic';
import TransactionsTable from '../../../components/transactions.table';
import {AccountReducerState} from '../../../interfaces/account.interface';
import CommonBalance from '../../../components/common.balance';
import LoadingFullScreen from "../../../components/loading.full.screen";

interface AccountHomeProps {
    windowSize: any,
    location: any,
    account: AccountReducerState,
    path: any
}

const AccountHomeRoute = ({windowSize, location, account, ...rest}: AccountHomeProps) => {

    return account.loading === false
        ? (
            <FlexGridAccount
                breadCrumbs={<BreadCrumbBasic location={location}/>}
                windowSize={windowSize}
                slotOne={
                    <TransactionsTable
                        transactions={account.data.transactions}
                        windowSize={windowSize}
                        handleLoadMore={()=> console.log("load more transactions")}
                    />
                }
                slotTwo={(<CommonBalance value={account.data.balance}/>)}
            />
        )
        : (
            <FlexGridAccount
                breadCrumbs={<BreadCrumbBasic location={location}/>}
                windowSize={windowSize}
                slotOne={<LoadingFullScreen/>}
                slotTwo={<LoadingFullScreen/>}
            />
        );
};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        location: state.router.location,
        account: state.account,
    };
};

export default connect(mapStateToProps)(AccountHomeRoute);

