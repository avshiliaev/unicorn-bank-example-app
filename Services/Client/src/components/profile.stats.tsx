import React from 'react';
import { Descriptions } from 'antd';
import CommonBalance from './common.balance';
import ProfileIcon from './profile.icon';

const ProfileStats = ({ windowSize, auth, balance }) => {

  const column = { xxl: 1, xl: 1, lg: 1, md: 1, sm: 1, xs: 1 };

  return (
    <Descriptions title="User Info" column={column}>
      <Descriptions.Item>
        <ProfileIcon size={60} id={auth.userId}/>
      </Descriptions.Item>
      <Descriptions.Item>
        <CommonBalance value={balance ? balance : 0}/>
      </Descriptions.Item>
    </Descriptions>
  );
};

export default ProfileStats;
