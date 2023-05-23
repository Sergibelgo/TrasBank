import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { AuthService } from '../../services/auth-service.service';

@Injectable()
export class UserEffects {
  loadUserJWT$ = createEffect(() => this.actions$.pipe(
    ofType("[User login] Try login"),
    mergeMap((action: any) => this.authService.login(action.password, action.username, action.email)
      .then((userJWT) => ({ type: "[User login] Set JWT", userJWT }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );

  constructor(
    private actions$: Actions,
    private authService: AuthService
  ) { }
}
