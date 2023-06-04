import { createAction, createActionGroup, props } from '@ngrx/store';
import { Loan } from '../../Models/loan/loan';


export const loadLoans = createAction("[Loans] Load loans", props<{ jwt: string }>());
export const setLoans = createAction("[Loans] Set loans", props<{loans:Loan[]}>())
