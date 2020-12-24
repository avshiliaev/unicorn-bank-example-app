import React from 'react';
import {Button, Descriptions} from 'antd';
import {Link} from '@reach/router';

const AccountsActions = () => {

    const column = {xxl: 1, xl: 1, lg: 1, md: 1, sm: 1, xs: 1};

    return (
        <div>
            <Descriptions title="Actions" column={column}/>
            <Button block>
                <Link to='/dashboard/new'>Add account</Link>
            </Button>
        </div>
    );
};

export default AccountsActions;
