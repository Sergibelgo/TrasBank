import { createReducer, on } from '@ngrx/store';
import { Enumsstate } from '../../Models/enumsState/enumsstate';
import { setAccountStatuses, setAccountTypes, setLoanTypes, setTransactionTypes, setWorkingStatuses } from '../actions/enums.actions';
import { state } from '@angular/animations';




export const initialState: Enumsstate = {
  AccountStatuses: [],
  LoanTypes: [],
  TransactionTypes: [],
  WorkerStatuses: [],
  WorkingStatuses: [],
  AccountTypes:[]
};

export const enumsReducer = createReducer(
  initialState,
  on(setTransactionTypes, (state, action) => {
    return { ...state, TransactionTypes: action.TransactionsTypes }
  }),
  on(setWorkingStatuses, (state, action) => {
    return { ...state, WorkingStatuses: action.WorkingStatuses }
  }),
  on(setAccountTypes, (state, action) => {
    return { ...state, AccountTypes: action.AccountTypes }
  }),
  on(setLoanTypes, (state, action) => {
    return { ...state, LoanTypes:action.LoanTypes }
  }),
  on(setAccountStatuses, (state, action) => {
    return {...state,AccountStatuses:action.AccountStatuses}
  })
);
