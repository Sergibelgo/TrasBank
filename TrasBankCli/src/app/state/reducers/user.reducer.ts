import { createReducer, on } from '@ngrx/store';
import { setError, setLoad, setUser, setUserJWT } from '../actions/auth.actions';
import { UserState } from '../../Models/userState/user-state';




export const initialState: UserState = {
  loading: false,
  userJWT: "",
  user: null,
  errorMsg:""
};

export const userReducer = createReducer(
  initialState,
  on(setLoad, (state, action) => {
    return { ...state, loading: action.load }
  }),
  on(setUserJWT, (state, action) => {
    return { ...state, userJWT: action.userJWT, loading: false }
  }),
  on(setError, (state, action) => {
    return { ...state, errorMsg: action.error, loading:false }
  }),
  on(setUser, (state, action) => {
    return { ...state, user: action.user }
  })
  
);
