import React from 'react';
import {connect} from 'react-redux';
import {Button, Col, Layout, Row, Spin} from 'antd';
import FlexContainer from './components/layout/flex.container';
import {useAuth0} from "@auth0/auth0-react";

const {Content} = Layout;

const Login = ({windowSize, ...rest}) => {

    const {isAuthenticated, loginWithRedirect, isLoading} = useAuth0();

    return (
        <Layout style={{minHeight: '100vh'}}>
            <Content style={{padding: windowSize.large ? 16 : 0}}>
                <Row
                    gutter={[{xs: 0, sm: 0, md: 0, lg: 16}, 24]}
                    align="middle"
                    justify="center"
                    style={{minHeight: '100vh'}}
                >
                    <Col xs={24} sm={24} md={18} lg={8} xl={6} xxl={6}>
                        <div style={{background: '#fff', padding: 24}}>
                            <div style={{height: "200px"}}>
                            <FlexContainer justify={'center'} align={'center'}>

                                    {!isAuthenticated && !isLoading &&
                                    <Button type="primary" onClick={() => loginWithRedirect()}>
                                        Log In
                                    </Button>
                                    }
                                    {isLoading &&
                                    <Spin size="large"/>
                                    }

                            </FlexContainer>
                            </div>
                        </div>
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};

const mapStateToProps = (state) => {
    return {
        windowSize: state.windowSize.greaterThan,
    };
};
const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(Login);

