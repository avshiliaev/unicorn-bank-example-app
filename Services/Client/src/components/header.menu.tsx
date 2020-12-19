import React from 'react';
import { Menu } from 'antd';
import { Link } from '@reach/router';

const HeaderMenu = ({ windowSize, location }) => {

  return (
    <Menu
      theme="light"
      mode='horizontal'
      selectedKeys={[location.pathname]}
      style={{
        lineHeight: windowSize.large ? '61px' : '53px',
      }}
    >
      <Menu.Item key="/dashboard/home">
        <Link to="/dashboard/home">
          <span>My Accounts</span>
        </Link>
      </Menu.Item>
      <Menu.Item key="/dashboard/discover">
        <Link to="/dashboard/discover">
          <span>Discover</span>
        </Link>
      </Menu.Item>
    </Menu>
  );
};

export default HeaderMenu;
