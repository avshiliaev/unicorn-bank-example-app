import React, { Fragment } from 'react';
import { Button, Descriptions, List } from 'antd';
import { ViewSettings } from '../interfaces/view.settings.interface';

interface Props {
  displayMore: any
  notifications: {
    title: string;
    description: string;
  }[]
}

const NotificationsList = (props: Props) => {

  const { notifications, displayMore } = props;

  return (
    <Fragment>
      <Descriptions title="Notifications"/>
      <List
        itemLayout="horizontal"
        dataSource={notifications}
        renderItem={item => (
          <List.Item>
            <List.Item.Meta
              title={<a href="https://ant.design">{item.title}</a>}
              description={item.description}
            />
          </List.Item>
        )}
      />
      <Button onClick={() => displayMore(notifications.length + 5)}>Display More</Button>
    </Fragment>
  );

};

export default NotificationsList;
