import { createReducer, on } from '@ngrx/store';
import { Enumsstate } from '../../Models/enumsState/enumsstate';
import { setTransactionTypes, setWorkingStatuses } from '../actions/enums.actions';
import { state } from '@angular/animations';
import { WorkerState } from '../../Models/WorkerState/worker-state';
import { setActiveUser, setCustomers, setPendingLoans, setPublicInfo } from '../actions/worker.actions';




export const initialState: WorkerState = {
  workersPublicInfo: [{ Email: "", Name: "" }],
  customers: [],
  loansPending: [],
  activeUser: ""
};

export const workerReducer = createReducer(
  initialState,
  on(setPublicInfo, (state, action) => {
    return { ...state, workersPublicInfo: action.workersPublicInfo }
  }),
  on(setCustomers, (state, action) => {
    return { ...state, customers: action.customers }
  }),
  on(setPendingLoans, (state, action) => {
    return { ...state, loansPending: action.loans }
  }),
  on(setActiveUser, (state, action) => {
    return {...state,activeUser:action.id}
  })
);
