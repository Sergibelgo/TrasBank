import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap, flatMap } from 'rxjs/operators';
import { AuthService } from '../../services/auth-service.service';
import { User } from '../../Models/User/user';
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
  registerUser$ = createEffect(() => this.actions$.pipe(
    ofType("[User register] Try register"),
    mergeMap((action: any) => this.authService.register(action)
      .then((userJWT) => ({ type: "[User login] Set JWT", userJWT }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )

  ))
  loadUser$ = createEffect(() => this.actions$.pipe(
    ofType("[User info] Load info"),
    mergeMap((action: any) => this.authService.loadUser(action.jwt)
      .then((user) => ({ type: "[User info] set info", user}))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ));

  constructor(
    private actions$: Actions,
    private authService: AuthService
  ) { }
}
