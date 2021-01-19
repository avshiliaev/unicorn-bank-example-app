import React, {Fragment} from 'react';
import {Button, Input, List, Space} from 'antd';
import {WindowSizeInterface} from "../interfaces/window.size.interface";
import FixedScrollContainer from "./layout/scroll.container";

interface Props {
    displayMore: any
    windowSize: WindowSizeInterface
    loading: boolean
    notifications: {
        title: string;
        description: string;
    }[]
}

const NotificationsList = (props: Props) => {

    const {notifications, displayMore, windowSize, loading} = props;

    const handleLoadMore = () => {
        console.log("LOAD MORE")
        displayMore(notifications.length + 5)
    }

    const refresh = () => {
        console.log("REFRESH")
    }

    return (
        <Fragment>
            <FixedScrollContainer
                handleLoadMore={handleLoadMore}
                dataLength={notifications.length}
                height={windowSize.large ? "calc(100vh - 265px)" : "calc(100vh - 265px)"}
            >
                <Space direction="vertical">
                    <Input placeholder="Profile"/>
                    <Button type="primary" block>
                        Submit
                    </Button>
                </Space>

                <List
                    loading={loading}
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
            </FixedScrollContainer>
        </Fragment>
    );

};

export default NotificationsList;
