import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { EnumsService } from '../../services/enums.service';

@Injectable()
export class EnumsEffects {
  transactionsTypes$ = createEffect(() => this.actions$.pipe(
    ofType("[Enums] load TransactionsTypes"),
    mergeMap((action: any) => this.enumsService.getTransactionTypes()
      .then((TransactionTypes) => ({ type: "[Enums] Set TransactionsTypes", TransactionTypes }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );
  workingStatuses$ = createEffect(() => this.actions$.pipe(
    ofType("[Enums] load WorkingStatuses"),
    mergeMap(
      (action: any) => this.enumsService.getWorkingStatuses()
        .then((WorkingStatuses) => ({ type: "[Enums] Set WorkingStatuses", WorkingStatuses }))
        .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  accountTypes$ = createEffect(() => this.actions$.pipe(
    ofType("[Enums] load AccountTypes"),
    mergeMap(
      () => this.enumsService.getAccountTypes()
        .then((AccountTypes) => ({ type: "[Enums] Set AccountTypes", AccountTypes }))
        .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  accountStatuses$ = createEffect(() => this.actions$.pipe(
    ofType("[Enums] load AccountStatuses"),
    mergeMap(
      () => this.enumsService.getAccountStatuses()
        .then((AccountStatuses) => ({ type: "[Enums] Set AccountStatuses", AccountStatuses }))
        .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  loanTypes$ = createEffect(() => this.actions$.pipe(
    ofType("[Enums] load LoanTypes"),
    mergeMap(
      () => this.enumsService.getLoansTypes()
        .then((LoanTypes) => ({ type: "[Enums] Set LoanTypes", LoanTypes }))
        .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))

  constructor(
    private actions$: Actions,
    private enumsService: EnumsService
  ) { }
}
