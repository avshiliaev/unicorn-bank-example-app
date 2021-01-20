import React from 'react';
import {Menu} from 'antd';
import {ContainerOutlined, PieChartOutlined, UserOutlined} from '@ant-design/icons';
import {Link} from '@reach/router';


const AccountSiderMenu = ({location}) => {

    const pathName = location.pathname.split("/");

    return (
        <Menu
            mode="inline"
            selectedKeys={[pathName[pathName.length - 1]]}
            defaultOpenKeys={['sub1']}
            style={{height: '100%', borderRight: 0, marginTop: 16}}
        >
            <Menu.Item key="home">
                <Link to="home">
                    <UserOutlined/>
                    <span>Overview</span>
                </Link>
            </Menu.Item>
            <Menu.Item key="transactions">
                <Link to="transactions">
                    <ContainerOutlined/>
                    <span>Transactions</span>
                </Link>
            </Menu.Item>
        </Menu>
    );
};

export default AccountSiderMenu;
