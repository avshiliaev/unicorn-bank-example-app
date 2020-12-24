import React from 'react';
import {Col, Row} from 'antd';

const MiddleColContent = ({slotOne, gutter, windowSize}) => {

    return (
        <Row
            gutter={[gutter, gutter]}
            justify="center"
        >
            <Col span={24}>
                <div
                    style={{background: '#fff', padding: 24}}
                >
                    {slotOne}
                </div>
            </Col>
        </Row>
    );
};

const RightColContent = ({slotTwo, gutter}) => {

    return (
        <Row
            gutter={[gutter, gutter]}
            justify="center"
        >
            <Col span={24}>
                <div style={{background: '#fff', padding: 24}}>
                    {slotTwo}
                </div>
            </Col>
        </Row>
    );
};

const FlexGridUser = ({windowSize, slotOne, slotTwo, breadCrumbs}) => {

    const gutter = {xs: 0, sm: 0, md: 0, lg: 16};

    const upperFlex = {
        colOne: {xs: 0, sm: 0, md: 0, lg: 24, xl: 24, xxl: 24},
        colTwo: {xs: 24, sm: 24, md: 24, lg: 24, xl: 18, xxl: 16},
        colThree: {xs: 0, sm: 0, md: 0, lg: 0, xl: 6, xxl: 8},
    };

    return (
        <Row
            gutter={[gutter, 16]}
            justify="center"
        >
            <Col
                xs={upperFlex.colOne.xs} sm={upperFlex.colOne.sm}
                md={upperFlex.colOne.md} lg={upperFlex.colOne.lg}
                xl={upperFlex.colOne.lg} xxl={upperFlex.colOne.xxl}
            >
                {breadCrumbs}
            </Col>
            <Col
                xs={upperFlex.colTwo.xs} sm={upperFlex.colTwo.sm}
                md={upperFlex.colTwo.md} lg={upperFlex.colTwo.lg}
                xl={upperFlex.colTwo.lg} xxl={upperFlex.colTwo.xxl}
            >
                <MiddleColContent
                    slotOne={slotOne}
                    windowSize={windowSize}
                    gutter={gutter}
                />
            </Col>
            <Col
                xs={upperFlex.colThree.xs} sm={upperFlex.colThree.sm}
                md={upperFlex.colThree.md} lg={upperFlex.colThree.lg}
                xl={upperFlex.colThree.lg} xxl={upperFlex.colThree.xxl}
            >
                <RightColContent
                    slotTwo={slotTwo}
                    gutter={gutter}
                />
            </Col>
        </Row>
    );
};

export default FlexGridUser;
