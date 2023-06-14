import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { LoanState } from '../../Models/loansState/loan-state';

export const selectLoanFeature = (state: AppState) => state.loans;

export const selectLoans = createSelector(
  selectLoanFeature,
  (state: LoanState) => state.loans
);
export const selectActiveLoan = createSelector(
  selectLoanFeature,
  (state: LoanState) => state.activeLoan
)
export const selectScoring = createSelector(
  selectLoanFeature,
  (state:LoanState)=>state.scoringForm
)
