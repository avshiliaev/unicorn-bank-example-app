import React from 'react';
import { Layout } from 'antd';

const { Sider } = Layout;

const SiderBasic = ({ children }) => {

  return (
    <Sider width={200} theme={'light'}>
      {children}
    </Sider>
  );
};

export default SiderBasic;
