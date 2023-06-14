import { createAction, createActionGroup, props } from '@ngrx/store';
import { Message } from '../../Models/message/message';


export const loadMessages = createAction("[Messages] Load messages", props<{ jwt: string }>());
export const setMessages = createAction("[Messages] Set messages", props<{ messages: Message[] }>());
export const setNotReaded = createAction("[Messages] Set notReaded");
export const resetMessages = createAction("[Messages] Reset");
export const setActiveMessage = createAction("[Messages] Set active", props<{ Id: string }>());
export const setReaded = createAction("[Messages] Set Readed", props<{ jwt: string, Id: string }>());
export const updateReaded = createAction("[Messages] Update Readed", props<{ Id: string }>())

