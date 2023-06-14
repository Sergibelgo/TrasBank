import { createReducer, on } from '@ngrx/store';
import { UtilsState } from '../../Models/utilsStatus/utils-state';
import { resetUtils, setError, setIndex, setLoad, setSuccess } from '../actions/utils.actions';




export const initialState: UtilsState = {
  loading: false,
  index: 0,
  errorMsg: "",
  success: ""
};

export const utilsReducer = createReducer(
  initialState,
  on(setLoad, (state, action) => {
    return { ...state, loading: action.load, errorMsg: "", success:"" }
  }), on(setError, (state, action) => {
    return { ...state, errorMsg: action.error, loading: false }
  }),
  on(setIndex, (state, action) => {
    return { ...state, index: action.index }
  }),
  on(setSuccess, (state, action) => {
    return { ...state, success: action.msg }
  }),
  on(resetUtils, () => {
    return initialState
  })
);
