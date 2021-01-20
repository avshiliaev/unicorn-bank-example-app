import React from "react";
import {connect} from "react-redux";
import FlexGridAccount from "../../../components/layout/flex.grid.account";
import BreadCrumbBasic from "../../../components/layout/breadcrumb.basic";
import TransactionsTable from "../../../components/transactions.table";
import CommonBalance from "../../../components/common.balance";
import LoadingFullScreen from "../../../components/loading.full.screen";
import {AccountReducerState} from "../../../interfaces/account.interface";
import DemoPlaceHolder from "../../../components/demo.placeholder";

interface AccountTransactionsProps {
    windowSize: any,
    location: any,
    account: AccountReducerState,
    path: any
}

const AccountTransactionsRoute = ({windowSize, location, account, ...rest}: AccountTransactionsProps) => {

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
                slotTwo={<DemoPlaceHolder/>}
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
}

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
        location: state.router.location,
        account: state.account,
    };
};

export default connect(mapStateToProps)(AccountTransactionsRoute);
