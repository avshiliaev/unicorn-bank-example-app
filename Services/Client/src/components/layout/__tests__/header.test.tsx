import React from 'react';
import {shallow} from 'enzyme';
import {cleanup} from '../../../../test-utils';
import HeaderBasic from '../header.basic';
import toJson from 'enzyme-to-json';

describe('Header', () => {
    // automatically unmount and cleanup DOM after the test is finished.
    afterEach(cleanup);

    it('renders without error', () => {

        const wrapper = shallow(
            <HeaderBasic
                windowSize={{large: true}}
                slotLeft={(<div>Left</div>)}
                slotMiddle={(<div>Middle</div>)}
                slotRight={(<div>Right</div>)}
            />,
        );
        expect(wrapper.contains(<div>Left</div>)).toBeTruthy();
        expect(wrapper.contains(<div>Middle</div>)).toBeTruthy();
        expect(wrapper.contains(<div>Right</div>)).toBeTruthy();

        expect(toJson(wrapper)).toMatchSnapshot()

    });
});
