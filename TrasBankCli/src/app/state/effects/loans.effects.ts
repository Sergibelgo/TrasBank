import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { LoansService } from '../../services/loans.service';

@Injectable()
export class LoansEffects {
  loadLoans$ = createEffect(() => this.actions$.pipe(
    ofType("[Loans] Load loans"),
    mergeMap((action: any) => this.loansService.loadLoansUser(action.jwt)
      .then((loans) => ({ type: "[Loans] Set loans", loans }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );
  loadLoansByUserId$ = createEffect(() => this.actions$.pipe(
    ofType("[Loans] Load loans by user id"),
    mergeMap((action: any) => this.loansService.getByCustomerId(action.jwt, action.id)
      .then((loans) => ({ type: "[Loans] Set loans", loans }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  constructor(
    private actions$: Actions,
    private loansService: LoansService
  ) { }
}
