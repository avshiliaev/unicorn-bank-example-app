import React, {Fragment} from 'react';
import {Badge, List, Space, Statistic} from 'antd';
import {Link} from '@reach/router';
import {TransactionInterface} from '../interfaces/transaction.interface';
import FixedScrollContainer from "./layout/scroll.container";

const TransactionsTable = ({transactions, windowSize, handleLoadMore}) => {

    const transactionsList: TransactionInterface[] = transactions;
    const Amount = ({value}) => {
        return (
            <Statistic
                value={value}
                precision={2}
                valueStyle={value > 0 ? {color: '#3f8600'} : {color: '#cf1322'}}
                prefix={value > 0 ? '+' : ''}
                suffix="€"
            />
        );
    };

    return (
        <Fragment>
            <FixedScrollContainer
                handleLoadMore={handleLoadMore}
                dataLength={transactionsList.length}
                height={windowSize.large ? "calc(100vh - 315px)" : "calc(100vh - 315px)"}
            >
                <List
                    itemLayout="horizontal"
                    dataSource={transactionsList}
                    renderItem={tr => (
                        <List.Item actions={[<Amount value={tr.amount}/>]}>
                            <List.Item.Meta
                                title={
                                    <Space>
                                        {tr.approved
                                            ? <Badge status="success"/>
                                            : <Badge status="processing"/>}
                                        <Link to={'../transactions/' + tr.id}>
                                            <div>{tr.info}</div>
                                        </Link>
                                    </Space>
                                }
                                description={tr.timestamp}
                            />
                        </List.Item>
                    )}
                />
            </FixedScrollContainer>
        </Fragment>
    );
};

export default TransactionsTable;
