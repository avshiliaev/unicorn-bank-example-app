import React from 'react';
import {Provider} from 'react-redux';
import configureStore from 'redux-mock-store';
import {cleanup} from '../../../../test-utils';
import {mount} from 'enzyme';
import {AuthState} from "@auth0/auth0-react/dist/auth-state";
import UserPage from "../user.page";


jest.mock('react', () => ({
    ...jest.requireActual('react'),
    useEffect: (f) => console.debug('USE_EFFECT'),
}));
jest.mock('@auth0/auth0-react', () => ({
    ...jest.requireActual('@auth0/auth0-react'),
    useAuth0: (f) => {
        const authState: AuthState = {
            isAuthenticated: true,
            isLoading: false,
            user: {
                email: "email",
                email_verified: true,
                name: "name",
                nickname: "nickname",
                picture: "picture",
                sub: "sub",
                updated_at: "updated_at"
            }
        }

        return authState;
    },
}));

describe('UserPage ', () => {

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
            router: {
                location: {pathname: '/user/sub'},
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
                <UserPage {...props}>
                    <div>Body</div>
                </UserPage>
            </Provider>,
        );

        expect(wrapper.find('Header')).toHaveLength(1);
        expect(wrapper.find('BasicDrawer')).toHaveLength(1);
        expect(wrapper.find('HeaderMenu')).toHaveLength(1);
        expect(wrapper.find('Content')).toHaveLength(1);
        expect(wrapper.find('Layout')).toHaveLength(2);
        expect(wrapper.find('FooterMobile')).toHaveLength(1);

        // console.debug(wrapper.props().children.props)
        expect(wrapper.props().store.getState()).toEqual(initialState);

        // console.debug(wrapper.find('DashboardPage').props())

    });

});


