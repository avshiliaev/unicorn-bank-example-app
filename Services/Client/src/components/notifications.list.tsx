import React, {Fragment} from 'react';
import {Descriptions, List} from 'antd';
import InfiniteScroll from "react-infinite-scroll-component";

interface Props {
    displayMore: any
    notifications: {
        title: string;
        description: string;
    }[]
}

const NotificationsList = (props: Props) => {

    const {notifications, displayMore} = props;

    const handleLoadMore = () => {
        console.log("LOAD MORE")
        displayMore(notifications.length + 5)
    }

    const refresh = () => {
        console.log("REFRESH")
    }

    const Messages = () => {
        return (
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
        )
    }

    return (
        <Fragment>
            <div
                id="scrollableDiv"
                style={{
                    height: "60vh",
                    overflow: 'auto',
                    display: 'flex',
                    flexDirection: 'column-reverse',
                }}
            >
                {/*Put the scroll bar always on the bottom*/}
                <InfiniteScroll
                    dataLength={notifications.length}
                    next={handleLoadMore}
                    style={{ display: 'flex', flexDirection: 'column-reverse' }} //To put endMessage and loader to the top.
                    inverse={true}
                    hasMore={true}
                    loader={<h4>Loading...</h4>}
                    scrollableTarget="scrollableDiv"
                >
                    <Messages/>
                </InfiniteScroll>
            </div>
        </Fragment>
    );

};

export default NotificationsList;
