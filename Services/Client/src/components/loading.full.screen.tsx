import React from "react";
import FlexContainer from "./layout/flex.container";
import {Spin} from "antd";

const LoadingFullScreen = () => {
    return (
        <FlexContainer justify={'center'} align={'center'}>
            <Spin size="large"/>
        </FlexContainer>
    )
}

export default LoadingFullScreen;
