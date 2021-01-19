import {Menu} from 'antd';
import React from 'react';
import {ContainerOutlined, PieChartOutlined, UserOutlined} from '@ant-design/icons';
import {Link} from '@reach/router';

const {SubMenu} = Menu;

const UserSiderMenu = ({windowSize, location}) => {

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
                    <span>Profile</span>
                </Link>
            </Menu.Item>
            <Menu.Item key="statistics">
                <Link to="statistics">
                    <PieChartOutlined/>
                    <span>Statistics</span>
                </Link>
            </Menu.Item>
            <Menu.Item key="messages">
                <Link to="messages">
                    <ContainerOutlined/>
                    <span>Messages</span>
                </Link>
            </Menu.Item>
        </Menu>
    );
};

export default UserSiderMenu;
