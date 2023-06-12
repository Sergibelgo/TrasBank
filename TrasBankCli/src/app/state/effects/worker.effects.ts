import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { WorkersService } from '../../services/workers.service';

@Injectable()
export class WorkerEffects {
  transactionsTypes$ = createEffect(() => this.actions$.pipe(
    ofType("[Worker] load public info"),
    mergeMap((action: any) => this.workerService.getPublicInfo()
      .then((workersPublicInfo) => ({ type: "[Worker] Set public info", workersPublicInfo }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );
  loadCustomers$ = createEffect(() => this.actions$.pipe(
    ofType("[Worker] Load customers"),
    mergeMap((action: any) => this.workerService.getCustomers(action.jwt)
      .then((customers) => ({ type: "[Worker] Set customers", customers }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ));
  loadPending$ = createEffect(() => this.actions$.pipe(
    ofType("[Worker] Load pending loans"),
    mergeMap((action: any) => this.workerService.getPending(action.jwt)
      .then((loans) => ({ type: "[Worker] Set pending loans", loans }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ));

  constructor(
    private actions$: Actions,
    private workerService: WorkersService
  ) { }
}
