import { createSelector } from '@ngrx/store';
import { AppState } from '../app.state';
import { Enumsstate } from '../../Models/enumsState/enumsstate';
import { WorkerState } from '../../Models/WorkerState/worker-state';

export const selectWorkerFeature = (state: AppState) => state.workers;

export const selectWorkersPublicInfo = createSelector(
  selectWorkerFeature,
  (state: WorkerState) => state.workersPublicInfo
);
