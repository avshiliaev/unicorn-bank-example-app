import React from 'react';
import {Breadcrumb} from 'antd';
import {HomeOutlined} from '@ant-design/icons';

const BreadCrumbBasic = ({location}) => {
    const path = location.pathname.split('/');
    return (
        <Breadcrumb>
            <Breadcrumb.Item href="/dashboard/home">
                <HomeOutlined/>
            </Breadcrumb.Item>
            {path.map(p => (<Breadcrumb.Item key={p}>{p}</Breadcrumb.Item>))}
        </Breadcrumb>
    );
};

export default BreadCrumbBasic;
