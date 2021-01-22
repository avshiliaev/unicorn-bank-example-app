import React from 'react';
import {Avatar, Badge, Descriptions, List, Space, Statistic} from 'antd';
import {Link} from '@reach/router';
import {AccountInterface} from '../interfaces/account.interface';
import {CheckCircleOutlined, CloseCircleOutlined, SyncOutlined} from '@ant-design/icons';

const AccountAvatar = ({badgeNumber, text}) => {

    return (
        <Badge count={badgeNumber}>
            <Avatar style={{backgroundColor: '#00a2ae'}}>{text}</Avatar>
        </Badge>
    );
};

const AccountAmount = ({value}) => {
    return (
        <Statistic
            value={value}
            precision={2}
            valueStyle={value > 0 ? {color: ''} : {color: '#cf1322'}}
            suffix="€"
        />
    );
};

const AccountTitle = ({approved, pending, blocked, link, title}) => {
    return (
        <Space>
            <Link to={link}>{title}</Link>
            {
                approved
                    ? <CheckCircleOutlined/>
                    : pending
                    ? <SyncOutlined spin/>
                    : blocked
                        ? <CloseCircleOutlined/>
                        : <CloseCircleOutlined/>
            }
        </Space>
    );
};

interface Props {
    accounts: AccountInterface[],
    windowSize: any,
    loading: boolean,
}

const AccountsList = ({accounts, windowSize, loading}: Props) => {

    return (
        <div>
            <Descriptions title="Accounts"/>
            <List
                loading={loading}
                itemLayout={!windowSize.large ? 'vertical' : 'horizontal'}
                dataSource={accounts}
                renderItem={account => {
                    const link = `/account/${account.accountId}/home`;
                    const description = account.accountId;
                    const {approved, pending, blocked} = account;
                    return (
                        <List.Item actions={[
                            <AccountAmount value={account.balance}/>,
                        ]}>
                            <List.Item.Meta
                                avatar={<AccountAvatar text={'G'} badgeNumber={account.transactions?.length}/>}
                                title={
                                    <AccountTitle
                                        approved={approved}
                                        pending={pending}
                                        blocked={blocked}
                                        link={link}
                                        title={'GiroX Konto+'}
                                    />
                                }
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
