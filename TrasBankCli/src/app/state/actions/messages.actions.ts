import { createAction, createActionGroup, props } from '@ngrx/store';
import { Message } from '../../Models/message/message';


export const loadMessages = createAction("[Messages] Load messages", props<{ jwt: string }>());
export const setMessages = createAction("[Messages] Set messages", props<{ messages: Message[] }>());
export const setNotReaded = createAction("[Messages] Set notReaded");
export const resetMessages = createAction("[Messages] Reset");

