import {Menu} from 'antd';
import React from 'react';
import {ContainerOutlined, PieChartOutlined, UserOutlined} from '@ant-design/icons';
import {Link} from '@reach/router';

const {SubMenu} = Menu;

const UserSiderMenu = () => {

    return (
        <Menu
            mode="inline"
            defaultSelectedKeys={['1']}
            defaultOpenKeys={['sub1']}
            style={{height: '100%', borderRight: 0, marginTop: 16}}
        >
            <Menu.Item key="1">
                <Link to="home">
                    <UserOutlined/>
                    <span>Profile</span>
                </Link>
            </Menu.Item>
            <Menu.Item key="2">
                <Link to="statistics">
                    <PieChartOutlined/>
                    <span>Statistics</span>
                </Link>
            </Menu.Item>
            <Menu.Item key="3">
                <Link to="entities">
                    <ContainerOutlined/>
                    <span>Entities</span>
                </Link>
            </Menu.Item>
        </Menu>
    );
};

export default UserSiderMenu;
