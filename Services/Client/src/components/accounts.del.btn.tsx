import React from 'react';
import {Button} from 'antd';

const AccountsDelBtn = ({mutate, variables}) => {

    return (
        <div>
            <Button size="small" onClick={() => mutate({variables})}>Delete</Button>
        </div>
    );
};

export default AccountsDelBtn;

