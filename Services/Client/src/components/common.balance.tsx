import React from 'react';
import { Statistic } from 'antd';

const CommonBalance = ({ value }) => {

  return (
    <Statistic title="Total Balance (EUR)" value={value} precision={2}/>
  );
};

export default CommonBalance;
