import { createAction, createActionGroup, props } from '@ngrx/store';
import { Loan } from '../../Models/loan/loan';
import { Scoring } from '../../Models/scoring/scoring';


export const loadLoans = createAction("[Loans] Load loans", props<{ jwt: string }>());
export const setLoans = createAction("[Loans] Set loans", props<{ loans: Loan[] }>());
export const setScoring = createAction("[Loans] Set scoring", props<{ scoring: Scoring }>())
export const addLoan = createAction("[Loans] Add loan", props<{ loan: Loan }>());
export const setActiveLoan = createAction("[Loans] Set active", props<{ id: string }>())

