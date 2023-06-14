import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { UtilsState } from '../../Models/utilsStatus/utils-state';

export const selectUtilFeature = (state: AppState) => state.utils;

export const selectLoading = createSelector(
  selectUtilFeature,
  (state: UtilsState) => state.loading
);
export const selectError = createSelector(
  selectUtilFeature,
  (state: UtilsState) => state.errorMsg
);
export const selectIndex = createSelector(
  selectUtilFeature,
  (state: UtilsState) => state.index
)
export const selectSuccess = createSelector(
  selectUtilFeature,
  (state: UtilsState) => state.success
)
