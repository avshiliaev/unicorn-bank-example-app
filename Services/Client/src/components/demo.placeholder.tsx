import {Col, Row} from "antd";
import React from "react";
import {InfoCircleOutlined} from '@ant-design/icons';

const DemoPlaceHolder = () => {

    return (
        <Row justify="center" align="middle" gutter={[{xs: 0, sm: 0, md: 0, lg: 16}, 0]}>
            <Col xs={24} sm={24} md={24} lg={24} xl={24} xxl={24}>
                <Row justify="center" align="middle" gutter={[{xs: 0, sm: 0, md: 0, lg: 16}, 0]}>
                    <Col>
                        <InfoCircleOutlined style={{fontSize: '24px', color: '#08c'}}/>
                    </Col>
                </Row>
                <Row justify="center" align="middle" gutter={[{xs: 0, sm: 0, md: 0, lg: 16}, 0]}>
                    <Col>
                        <div> This is a demo application</div>
                    </Col>
                </Row>
            </Col>
        </Row>
    )
}

export default DemoPlaceHolder;
