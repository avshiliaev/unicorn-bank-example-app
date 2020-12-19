import React from 'react';
import { Col, Row } from 'antd';
import CSS from 'csstype';

const LeftColContent = ({ slotOne, slotTwo, windowSize, flexGrid, gutter }) => {

  const contentStyle: CSS.Properties = {
    background: '#fff',
    // marginTop: windowSize.large ? 'auto' : '64px',
  };

  return (
    <Row
      gutter={[gutter, gutter]}
      justify="center"
    >
      <Col
        xs={flexGrid.colOne.xs} sm={flexGrid.colOne.sm}
        md={flexGrid.colOne.md} lg={flexGrid.colOne.lg}
        xl={flexGrid.colOne.lg} xxl={flexGrid.colOne.xxl}
      >
        <div style={{ ...contentStyle, padding: 24 }}>
          {slotOne}
        </div>
      </Col>
      <Col
        xs={flexGrid.colTwo.xs} sm={flexGrid.colTwo.sm}
        md={flexGrid.colTwo.md} lg={flexGrid.colTwo.lg}
        xl={flexGrid.colTwo.lg} xxl={flexGrid.colTwo.xxl}
      >
        <div style={{ ...contentStyle, padding: 24 }}>
          {slotTwo}
        </div>
      </Col>
    </Row>
  );
};

const MiddleColContent = ({ mainContent, gutter }) => {

  return (
    <Row
      gutter={[gutter, gutter]}
      justify="center"
    >
      <Col span={24}>
        <div style={{ background: '#fff', padding: 24 }}>
          <div>{mainContent}</div>
        </div>
      </Col>
    </Row>
  );
};

const RightColContent = ({ slotThree, gutter }) => {

  return (
    <Row
      gutter={[gutter, gutter]}
      justify="center"
    >
      <Col span={24}>
        <div style={{ background: '#fff', padding: 24 }}>
          {slotThree}
        </div>
      </Col>
    </Row>
  );
};

const FlexGridDashboard = ({ windowSize, slotOne, slotTwo, slotThree, mainContent }) => {

  const gutter = { xs: 0, sm: 0, md: 0, lg: 16 };

  const upperFlex = {
    colOne: { xs: 24, sm: 24, md: 24, lg: 8, xl: 6, xxl: 6 },
    colTwo: { xs: 24, sm: 24, md: 24, lg: 16, xl: 18, xxl: 12 },
    colThree: { xs: 0, sm: 0, md: 0, lg: 0, xl: 0, xxl: 6 },
  };

  const leftColFlex = {
    colOne: { xs: 24, sm: 12, md: 12, lg: 24, xl: 24, xxl: 24 },
    colTwo: { xs: 0, sm: 12, md: 12, lg: 24, xl: 24, xxl: 24 },
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
        <LeftColContent
          gutter={gutter}
          flexGrid={leftColFlex}
          slotOne={slotOne}
          slotTwo={slotTwo}
          windowSize={windowSize}
        />
      </Col>
      <Col
        xs={upperFlex.colTwo.xs} sm={upperFlex.colTwo.sm}
        md={upperFlex.colTwo.md} lg={upperFlex.colTwo.lg}
        xl={upperFlex.colTwo.lg} xxl={upperFlex.colTwo.xxl}
      >
        <MiddleColContent
          gutter={gutter}
          mainContent={mainContent}
        />
      </Col>
      <Col
        xs={upperFlex.colThree.xs} sm={upperFlex.colThree.sm}
        md={upperFlex.colThree.md} lg={upperFlex.colThree.lg}
        xl={upperFlex.colThree.lg} xxl={upperFlex.colThree.xxl}
      >
        <RightColContent
          gutter={gutter}
          slotThree={slotThree}
        />
      </Col>
    </Row>
  );
};

export default FlexGridDashboard;
