import React, {useState} from 'react';
import {Button, Drawer} from 'antd';
import {MenuOutlined} from '@ant-design/icons';

const BasicDrawer = ({children}) => {

    const [state, toggle] = useState(false);

    const ToggleButton = ({toggle, state}) => {
        return (
            <Button onClick={() => toggle(!state)} shape="circle">
                <MenuOutlined/>
            </Button>
        );
    };

    return (
        <div>
            <ToggleButton state={state} toggle={toggle}/>
            <Drawer
                bodyStyle={{padding: '0'}}
                title={<ToggleButton state={state} toggle={toggle}/>}
                placement={'left'}
                closable={false}
                onClose={() => toggle(!state)}
                visible={state}
            >
                {children}
            </Drawer>
        </div>
    );
};

export default BasicDrawer;
