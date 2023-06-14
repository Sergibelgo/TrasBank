import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { Enumsstate } from '../../Models/enumsState/enumsstate';

export const selectEnumFeature = (state: AppState) => state.enums;

export const selectWorkingStatuses = createSelector(
  selectEnumFeature,
  (state: Enumsstate) => state.WorkingStatuses
);
export const selectAccountTypes = createSelector(
  selectEnumFeature,
  (state: Enumsstate) => state.AccountTypes
)
export const selectLoanTypes = createSelector(
  selectEnumFeature,
  (state: Enumsstate) => state.LoanTypes
)
export const selectAccountStatuses = createSelector(
  selectEnumFeature,
  (state:Enumsstate)=>state.AccountStatuses
)
