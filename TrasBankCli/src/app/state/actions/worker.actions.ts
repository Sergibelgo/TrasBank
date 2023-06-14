import { createAction, createActionGroup, props } from '@ngrx/store';
import { WorkerPublicInfo } from '../../Models/Worker/worker';
import { User } from '../../Models/User/user';
import { Loan } from '../../Models/loan/loan';

export const loadPublicInfo = createAction("[Worker] load public info");
export const setPublicInfo = createAction("[Worker] Set public info", props<{ workersPublicInfo: WorkerPublicInfo[] }>());
export const loadCustomers = createAction("[Worker] Load customers", props<{ jwt: string }>());
export const setCustomers = createAction("[Worker] Set customers", props<{ customers: User[] }>())
export const loadPendingLoans = createAction("[Worker] Load pending loans", props<{ jwt: string }>());
export const setPendingLoans = createAction("[Worker] Set pending loans", props<{ loans: Loan[] }>());
export const setActiveUser = createAction("[Worker] Set active user", props < {id:string}>())
