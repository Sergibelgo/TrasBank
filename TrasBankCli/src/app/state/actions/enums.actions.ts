import { createAction, createActionGroup, props } from '@ngrx/store';
import { Enum } from '../../Models/enumsState/enumsstate';

export const loadTransactionTypes = createAction("[Enums] load TransactionsTypes");
export const setTransactionTypes = createAction("[Enums] Set TransactionsTypes", props<{ TransactionsTypes: Enum[] }>());
export const loadWorkingStatuses = createAction("[Enums] load WorkingStatuses");
export const setWorkingStatuses = createAction("[Enums] Set WorkingStatuses", props<{ WorkingStatuses: Enum[] }>());
export const loadAccountTypes = createAction("[Enums] load AccountTypes");
export const setAccountTypes = createAction("[Enums] Set AccountTypes", props<{ AccountTypes: Enum[] }>());
export const setAccountStatuses = createAction("[Enums] Set AccountStatuses");
export const setLoanTypes = createAction("[Enums] Set LoanTypes");
