import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { Enumsstate } from '../../Models/enumsState/enumsstate';
import { AccountState } from '../../Models/accountState/account-state';

export const selectAccountFeature = (state: AppState) => state.accounts;

export const selectAccounts = createSelector(
  selectAccountFeature,
  (state: AccountState) => state.accounts
);
export const selectAccountActive = createSelector(
  selectAccountFeature,
  (state: AccountState) => state.active
)
export const selectTransactions = createSelector(
  selectAccountFeature,
  (state:AccountState)=>state.transactions
)
