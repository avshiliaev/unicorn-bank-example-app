import React from "react";
import InfiniteScroll from "react-infinite-scroll-component";

const FixedScrollContainer = ({handleLoadMore, dataLength, children, height}) => {
    return (
        <div
            id="scrollableDiv"
            style={{
                height,
                overflow: 'auto',
                display: 'flex',
                flexDirection: 'column-reverse',
            }}
        >
            {/*Put the scroll bar always on the bottom*/}
            <InfiniteScroll
                dataLength={dataLength}
                next={handleLoadMore}
                style={{display: 'flex', flexDirection: 'column-reverse'}} //To put endMessage and loader to the top.
                inverse={true}
                hasMore={true}
                loader={<h4>Loading...</h4>}
                scrollableTarget="scrollableDiv"
            >
                {children}
            </InfiniteScroll>
        </div>
    )
}

export default FixedScrollContainer;
