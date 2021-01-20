import React from "react";
import {Line} from '@ant-design/charts';
import { Column } from '@ant-design/charts';
import {TransactionInterface} from "../interfaces/transaction.interface";

const TransactionsLinePlot = ({transactions}: { transactions: TransactionInterface[] }) => {

    const data = transactions.map(t => ({num: t.sequentialNumber.toString(), amount: t.amount}));

    const dataCumulative = data.map(
        (elem, index) =>
            data
                .slice(0, index + 1)
                .reduce(
                    (a, b) =>
                        ({num: b.num, amount: a.amount + b.amount})
                )
    );

    const config = {
        data: dataCumulative,
        xField: 'num',
        yField: 'amount',
    };

    return (
        <Column {...config} />
    )
};

export default TransactionsLinePlot;
