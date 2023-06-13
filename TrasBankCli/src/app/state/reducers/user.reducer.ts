import { createReducer, on } from '@ngrx/store';
import { resetUser,  setUser, setUserJWT, updateUserInfo } from '../actions/auth.actions';
import { UserState } from '../../Models/userState/user-state';




export const initialState: UserState = {
  userJWT: "",
  user: { Age: new Date(), Email: "", FirstName: "", Income: 0, LastName: "", UserName: "", Address: "", Id: "" }
}

export const userReducer = createReducer(
  initialState,
  on(setUserJWT, (state, action) => {
    return { ...state, userJWT: action.userJWT, loading: false }
  }),
  on(setUser, (state, action) => {
    return { ...state, user: action.user }
  }), on(resetUser, (state, action) => {
    return initialState
  }),
  on(updateUserInfo, (state, action) => {
    return { ...state, user: action.user }
  })
  
);
