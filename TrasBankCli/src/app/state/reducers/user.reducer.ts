import { createReducer, on } from '@ngrx/store';
import { resetUser,  setUser, setUserJWT } from '../actions/auth.actions';
import { UserState } from '../../Models/userState/user-state';




export const initialState: UserState = {
  userJWT: "",
  user: null
};

export const userReducer = createReducer(
  initialState,
  on(setUserJWT, (state, action) => {
    return { ...state, userJWT: action.userJWT, loading: false }
  }),
  on(setUser, (state, action) => {
    return { ...state, user: action.user }
  }), on(resetUser, (state, action) => {
    return initialState
  })
  
);
