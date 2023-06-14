import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { WorkersService } from '../../services/workers.service';
import { MessagesService } from '../../services/messages.service';

@Injectable()
export class MessagesEffects {
  loadMessages$ = createEffect(() => this.actions$.pipe(
    ofType("[Messages] Load messages"),
    mergeMap((action: any) => this.messageService.getMessages(action.jwt)
      .then((messages) => ({ type: "[Messages] Set messages", messages }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );
  changeReaded$ = createEffect(() => this.actions$.pipe(
    ofType("[Messages] Set Readed"),
    mergeMap((action: any) => this.messageService.setReaded(action.jwt,action.Id)
      .then((Id) => ({ type: "[Messages] Update Readed" ,Id}))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );
  setReaded = createEffect(() => this.actions$.pipe(
    ofType("[Messages] Set messages"),
    map((action: any) => {
      return ({ type: "[Messages] Set notReaded" })
    })
  ))
  constructor(
    private actions$: Actions,
    private messageService: MessagesService
  ) { }
}
