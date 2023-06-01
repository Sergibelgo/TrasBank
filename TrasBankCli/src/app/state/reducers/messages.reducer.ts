import { createReducer, on } from '@ngrx/store';
import { MessagesState } from '../../Models/messagesState/messages-state';
import { resetMessages, setMessages, setNotReaded } from '../actions/messages.actions';




export const initialState: MessagesState = {
  notReaded: false,
  messages:[]
};

export const messagesReducer = createReducer(
  initialState,
  on(setMessages, (state, action) => {
    return { ...state, messages: action.messages }
  }),
  on(setNotReaded, (state, action) => {
    let check = state.messages.some((item) => item.IsReaded == false);
    return { ...state, notReaded: check }
  }),
  on(resetMessages, (state, action) => {
    return initialState;
  })
);
