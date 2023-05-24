import { createReducer, on } from '@ngrx/store';
import { Enumsstate } from '../../Models/enumsState/enumsstate';
import { setTransactionTypes } from '../actions/enums.actions';
import { state } from '@angular/animations';




export const initialState: Enumsstate = {
  AccountStatuses: [{ id: 0, Name: "" }],
  LoanTypes: [{ id: 0, Name: "" }],
  TransactionTypes: [{ id: 0, Name: "" }],
  WorkerStatuses: [{ id: 0, Name: "" }],
  WorkingStatuses: [{ id: 0, Name: "" }]
};

export const enumsReducer = createReducer(
  initialState,
  on(setTransactionTypes, (state, action) => {
    return { ...state, TransactionTypes: action.TransactionsTypes }
  })
);
