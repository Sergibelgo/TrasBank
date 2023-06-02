import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { MessagesState } from '../../Models/messagesState/messages-state';

export const selectMessageFeature = (state: AppState) => state.messages;

export const selectMessages = createSelector(
  selectMessageFeature,
  (state: MessagesState) => state.messages
);
export const selectNotReaded = createSelector(
  selectMessageFeature,
  (state: MessagesState) => state.notReaded
)
export const selectActiveMessage = createSelector(
  selectMessageFeature,
  (state: MessagesState) => state.activeMessage
)
