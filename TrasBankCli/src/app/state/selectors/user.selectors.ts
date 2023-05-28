import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { UserState } from '../../Models/userState/user-state';

export const selectUserFeature = (state: AppState) => state.user;

export const selectJWT = createSelector(
  selectUserFeature,
  (state: UserState) => state.userJWT
);
export const selectLoading = createSelector(
  selectUserFeature,
  (state: UserState) => state.loading
);
export const selectError = createSelector(
  selectUserFeature,
  (state: UserState) => state.errorMsg
);
export const selectUser = createSelector(
  selectUserFeature,
  (state: UserState) => state.user
)
