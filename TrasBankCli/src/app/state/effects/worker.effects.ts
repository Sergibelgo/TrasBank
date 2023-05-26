import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { WorkersService } from '../../services/workers.service';

@Injectable()
export class WorkerEffects {
  transactionsTypes$ = createEffect(() => this.actions$.pipe(
    ofType("[Worker] load public info"),
    mergeMap((action: any) => this.enumsService.getPublicInfo()
      .then((workersPublicInfo) => ({ type: "[Worker] Set public info", workersPublicInfo }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );

  constructor(
    private actions$: Actions,
    private enumsService: WorkersService
  ) { }
}
