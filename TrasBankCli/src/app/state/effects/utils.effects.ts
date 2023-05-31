import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TranslateService } from '@ngx-translate/core';
import { map } from 'rxjs/operators';
@Injectable()
export class UtilsEffects {
  changeLoad1$ = createEffect(() => this.actions$.pipe(
    ofType("[Accounts] add account"),
    map((action:any) => {
      return ({ type: "[Utils] Load", load:false })
    })
  ))
  changeSuccessAccount = createEffect(() => this.actions$.pipe(
    ofType("[Accounts] add account"),
    map((action: any) => {
      return ({ type: "[Dash]set success", msg: this.transService.instant("Account added succesfuly") })
    })
  ))
  changeSuccessLog = createEffect(() => this.actions$.pipe(
    ofType("[User login] Set JWT"),
    map((action: any) => {
      return ({ type: "[Dash]set success", msg: this.transService.instant("Loged in successfully") })
    })
  ))

  constructor(
    private actions$: Actions,
    private transService: TranslateService
  ) { }
}
