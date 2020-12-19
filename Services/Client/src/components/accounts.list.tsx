import React from 'react';
import { Avatar, Badge, Descriptions, List, Space, Statistic } from 'antd';
import { Link } from '@reach/router';
import { AccountInterface } from '../interfaces/account.interface';
import { CheckCircleOutlined, SyncOutlined } from '@ant-design/icons';

const AccountAvatar = ({ badgeNumber, text }) => {

  return (
    <Badge count={badgeNumber}>
      <Avatar style={{ backgroundColor: '#00a2ae' }}>{text}</Avatar>
    </Badge>
  );
};

const AccountAmount = ({ value }) => {
  return (
    <Statistic
      value={value}
      precision={2}
      valueStyle={value > 0 ? { color: '' } : { color: '#cf1322' }}
      suffix="€"
    />
  );
};

const AccountTitle = ({ status, link, title }) => {
  return (
    <Space>
      <Link to={link}>{title}</Link>
      {status === 'approved' ? <CheckCircleOutlined/> : <SyncOutlined spin/>}
    </Space>
  );
};

interface Props {
  accounts: AccountInterface[],
  windowSize: any,
  loading: boolean,
}

const AccountsList = ({ accounts, windowSize, loading }: Props) => {

  return (
    <div>
      <Descriptions title="Accounts"/>
      <List
        loading={loading}
        itemLayout={!windowSize.large ? 'vertical' : 'horizontal'}
        dataSource={accounts}
        renderItem={account => {
          const link = `/account/${account.uuid}/home`;
          const description = 'DE34 0932 2356 9305 04';
          const status = account.status;
          return (
            <List.Item actions={[
              <AccountAmount value={account.balance}/>,
            ]}>
              <List.Item.Meta
                avatar={<AccountAvatar text={'G'} badgeNumber={account.transactions?.length}/>}
                title={<AccountTitle status={account.status} link={link} title={'GiroX Konto+'}/>}
                description={description}
              />
            </List.Item>
          );
        }}
      />
    </div>
  );
};

export default AccountsList;
