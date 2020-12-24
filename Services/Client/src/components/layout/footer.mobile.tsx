import React from 'react';
import CSS from 'csstype';
import {BackTop, Col, Layout, Row} from 'antd';
import {ContainerOutlined, MessageOutlined, UserOutlined} from '@ant-design/icons';
import {Link} from '@reach/router';

const {Footer} = Layout;

const FooterMobile = ({auth, location}) => {

    const headerShadow = '0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19)';

    const footerHeight = 56;
    const iconSize = 24;
    const paddingTop = Math.floor((footerHeight - iconSize) / 2);
    const iconStyles: CSS.Properties = {fontSize: `${iconSize}px`};

    return (
        <Footer style={
            {
                background: '#fff',
                position: 'sticky',
                bottom: '0',
                boxShadow: headerShadow,
                height: `${footerHeight}px`,
                paddingTop: `${paddingTop}px`
            }
        }>
            <Row align="middle">
                <Col span={8}>
                    <Row justify="start" align="middle">
                        <Col>
                            <Link to="/dashboard/home">
                                <ContainerOutlined style={
                                    {...iconStyles, color: location.pathname === "/dashboard/home" ? '#1890ff' : 'grey'}
                                }/>
                            </Link>
                        </Col>
                    </Row>
                </Col>
                <Col span={8}>
                    <Row justify="center" align="middle">
                        <Col>
                            <Link to={`/user/${auth.userId}/messages`}>
                                <MessageOutlined style={
                                    {
                                        ...iconStyles,
                                        color: location.pathname === `/user/${auth.userId}/messages` ? '#1890ff' : 'grey'
                                    }
                                }/>
                            </Link>
                        </Col>
                    </Row>
                </Col>
                <Col span={8}>
                    <Row justify="end" align="middle">
                        <Col>
                            <Link to={`/user/${auth.userId}/home`}>
                                <UserOutlined style={
                                    {
                                        ...iconStyles,
                                        color: location.pathname === `/user/${auth.userId}/home` ? '#1890ff' : 'grey'
                                    }
                                }/>
                            </Link>
                        </Col>
                    </Row>
                </Col>
            </Row>
            <BackTop/>
        </Footer>
    );
};

export default FooterMobile;
