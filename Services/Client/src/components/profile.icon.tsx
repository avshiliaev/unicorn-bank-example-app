import React from 'react';
import {Avatar} from 'antd';
import {UserOutlined} from '@ant-design/icons';
import {Link} from '@reach/router';

const ProfileIcon = ({size, id, image}) => {

    const link = `/user/${id}/home`;

    return (
        <Link to={link}>
            <Avatar size={size} icon={<UserOutlined/>} src={image}/>
        </Link>
    );
};

export default ProfileIcon;
