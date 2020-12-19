import React from 'react';

import { Drawer } from 'antd';

class DrawerMobile extends React.Component {
  state = { visible: false };

  showDrawer = () => {
    this.setState({
      visible: true,
    });
  };

  onClose = () => {
    this.setState({
      visible: false,
    });
  };

  render() {
    return (
      <div>
        <div onClick={this.showDrawer} style={{ fontSize: '16px' }}>icon</div>
        <Drawer
          title={<div>Lagerist</div>}
          placement="left"
          bodyStyle={{ padding: 0 }}
          closable={true}
          onClose={this.onClose}
          visible={this.state.visible}
        >
          {this.props.children}
        </Drawer>
      </div>
    );
  }
}

export default DrawerMobile;
