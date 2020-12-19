import userService from '../../services/user.service';
import { getUser } from '../user.reducer';
import { UserInterface } from '../../interfaces/user.interface';
import ActionTypes from '../../constants';

describe('async actions', () => {

  it(ActionTypes.GET_USER, async () => {
    const mockUser: UserInterface = {
      id: '0x1',
      username: 'name',
      accounts: [
        { uuid: '0x1', title: 't', balance: 0, status: 'approved' }
      ],
    };

    userService.getUser = jest.fn().mockReturnValue(mockUser);

    const dispatch = jest.fn();
    await getUser('0x1')(dispatch);
    expect(dispatch).toHaveBeenLastCalledWith({
      type: ActionTypes.GET_USER,
      data: mockUser,
    });
  });
});
