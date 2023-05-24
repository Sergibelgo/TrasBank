import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { EnumsService } from '../../services/enums.service';

@Injectable()
export class EnumsEffects {
  loadUserJWT$ = createEffect(() => this.actions$.pipe(
    ofType("[Enums] load TransactionsTypes"),
    mergeMap((action: any) => this.enumsService.getTransactionTypes()
      .then((TransactionTypes) => ({ type: "[Enums] Set TransactionsTypes", TransactionTypes }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );

  constructor(
    private actions$: Actions,
    private enumsService: EnumsService
  ) { }
}
