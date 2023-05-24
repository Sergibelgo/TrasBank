import { createAction, createActionGroup, props } from '@ngrx/store';
import { Enum } from '../../Models/enumsState/enumsstate';

export const loadTransactionTypes = createAction("[Enums] load TransactionsTypes");
export const setTransactionTypes = createAction("[Enums] Set TransactionsTypes", props<{ TransactionsTypes: Enum[] }>());
export const setWorkingStatuses = createAction("[Enums] Set WorkingStatuses");
export const setAccountStatuses = createAction("[Enums] Set AccountStatuses");
export const setLoanTypes = createAction("[Enums] Set LoanTypes");
