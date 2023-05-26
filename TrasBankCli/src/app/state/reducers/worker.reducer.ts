import { createReducer, on } from '@ngrx/store';
import { Enumsstate } from '../../Models/enumsState/enumsstate';
import { setTransactionTypes, setWorkingStatuses } from '../actions/enums.actions';
import { state } from '@angular/animations';
import { WorkerState } from '../../Models/WorkerState/worker-state';
import { setPublicInfo } from '../actions/worker.actions';




export const initialState: WorkerState = {
  workersPublicInfo: [{ Email: "", Name: "" }],
};

export const workerReducer = createReducer(
  initialState,
  on(setPublicInfo, (state, action) => {
    return { ...state, workersPublicInfo: action.workersPublicInfo }
  })
);
