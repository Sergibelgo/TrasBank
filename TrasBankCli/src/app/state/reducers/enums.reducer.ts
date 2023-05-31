import { createReducer, on } from '@ngrx/store';
import { Enumsstate } from '../../Models/enumsState/enumsstate';
import { setAccountTypes, setTransactionTypes, setWorkingStatuses } from '../actions/enums.actions';
import { state } from '@angular/animations';




export const initialState: Enumsstate = {
  AccountStatuses: [{ Id: 0, Name: "" }],
  LoanTypes: [{ Id: 0, Name: "" }],
  TransactionTypes: [{ Id: 0, Name: "" }],
  WorkerStatuses: [{ Id: 0, Name: "" }],
  WorkingStatuses: [{ Id: 0, Name: "" }],
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
  })
);
