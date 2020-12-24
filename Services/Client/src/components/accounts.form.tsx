import React from 'react';
import {Button, Form, Input} from 'antd';

const {TextArea} = Input;

const AccountsForm = ({onFinish}) => {

    const onFinishFailed = errorInfo => {
        console.log('Failed:', errorInfo);
    };

    return (
        <Form
            name="basic"
            initialValues={{remember: true}}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
        >
            <Form.Item
                name={'profile'}
                rules={[{
                    required: true,
                    message: 'Please input a valid profile (max 60 char.)',
                    min: 1,
                    max: 60,
                    whitespace: true,
                }]}>
                <Input
                    placeholder="Profile"
                />
            </Form.Item>
            <Form.Item
                name={'description'}
                rules={[{
                    required: true,
                    message: 'Please input a valid description (max 240 char.)',
                    min: 1,
                    max: 240,
                    whitespace: true,
                }]}>
                <TextArea
                    rows={4}
                    placeholder="Description"
                />
            </Form.Item>
            <Form.Item label={''}>
                <Button type="primary" htmlType="submit">
                    Submit
                </Button>
            </Form.Item>
        </Form>
    );
};

export default AccountsForm;
