import { createAction, createActionGroup, props } from '@ngrx/store';
import { WorkerPublicInfo } from '../../Models/Worker/worker';

export const loadPublicInfo = createAction("[Worker] load public info");
export const setPublicInfo = createAction("[Worker] Set public info", props<{ workersPublicInfo: WorkerPublicInfo[] }>());
