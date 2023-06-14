import { createReducer, on } from '@ngrx/store';
import { MessagesState } from '../../Models/messagesState/messages-state';
import { resetMessages, setActiveMessage, setMessages, setNotReaded, updateReaded } from '../actions/messages.actions';




export const initialState: MessagesState = {
  notReaded: false,
  messages: [],
  activeMessage: { Body: "", Date: new Date(), Id: "", IsReaded: true, SenderName:"",Title:"" }
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
  }),
  on(setActiveMessage, (state, action) => {
    let active = state.messages.find(item => item.Id == action.Id );
    return { ...state, activeMessage: (active ?? { Body: "", Date: new Date(), Id: "", IsReaded: true, SenderName: "", Title: "" }) }
  }),
  on(updateReaded, (state, action) => {
    return {
      ...state, messages: state.messages.map(item => {
        if (item.Id == action.Id) {
          return { ...item, IsReaded:true }
        }
        return item;
      })
    }
  })
);
