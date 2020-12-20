import React from 'react';
import {Provider} from 'react-redux';
import configureStore from 'redux-mock-store';
import {cleanup} from '../../../../test-utils';
import DashboardPage from '../dashboard.page';
import {mount} from 'enzyme';


jest.mock('react', () => ({
    ...jest.requireActual('react'),
    useEffect: (f) => console.debug('USE_EFFECT'),
}));

describe('DashboardPage ', () => {

    afterEach(cleanup);

    it('renders without error on mobile', async () => {

        const initialState = {
            windowSize: {
                greaterThan: {
                    extraSmall: false,
                    small: true,
                    medium: false,
                    large: false,
                    extraLarge: false,
                },
            },
            auth: {isLoggedIn: true, userId: '1', userName: 'testUser'},
            router: {
                location: {pathname: '/dashboard/home'},
            },
        };

        const mockStore = configureStore();
        const store = mockStore({...initialState});

        const props = {
            path: "",
            initAccountsOverview: jest.fn(),
        };

        const wrapper = mount(
            <Provider store={store}>
                <DashboardPage {...props}>
                    <div>Body</div>
                </DashboardPage>
            </Provider>,
        );

        expect(wrapper.find('Header')).toHaveLength(1);
        expect(wrapper.find('AppLogo')).toHaveLength(1);
        expect(wrapper.find('HeaderMenu')).toHaveLength(1);
        expect(wrapper.find('Content')).toHaveLength(1);
        expect(wrapper.find('Layout')).toHaveLength(2);
        expect(wrapper.find('FooterMobile')).toHaveLength(1);

        // console.debug(wrapper.props().children.props)
        expect(wrapper.props().store.getState()).toEqual(initialState);

        console.debug(wrapper.find('DashboardPage').props())

    });

});


