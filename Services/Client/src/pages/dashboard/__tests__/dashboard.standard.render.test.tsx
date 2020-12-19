import React from 'react';
import {Provider} from 'react-redux';
import configureStore from 'redux-mock-store';
import DashboardPage from '../dashboard.page';
import thunk from 'redux-thunk';
import {act} from 'react-dom/test-utils';
import ReactDOM from 'react-dom';

let container;

beforeEach(() => {
    container = document.createElement('div');
    document.body.appendChild(container);
});

afterEach(() => {
    document.body.removeChild(container);
    container = null;
});

it('can render and update a counter', () => {
    const mockStore = configureStore([thunk]);
    const store = mockStore({
        windowSize: {
            greaterThan: {
                extraSmall: false,
                small: false,
                medium: false,
                large: true,
                extraLarge: false,
            },
        },
        auth: {isLoggedIn: true, userId: '1', userName: 'testUser'},
        router: {
            location: {pathname: '/dashboard/home'},
        },
    });

    const props = {
        initAccountsOverview: jest.fn(),
    };

    // Test first render and componentDidMount
    act(() => {
        ReactDOM.render(
            <Provider store={store}>
                <DashboardPage {...props}>
                    <div>Body</div>
                </DashboardPage>
            </Provider>,
            container);
    });
    const header = container.querySelector('Header');
    expect(header).toBeTruthy();
});
