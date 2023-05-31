import { createAction, createActionGroup, props } from '@ngrx/store';
import { User } from '../../Models/User/user';
import { UserRegister } from '../../Models/UserRegister/user-register';

export const setUserJWT = createAction("[User login] Set JWT", props<{ userJWT: string }>());
export const tryLogIn = createAction("[User login] Try login", props<{ password: string, username?: string, email?: string }>());
export const tryRegister = createAction("[User register] Try register", props<{ data: UserRegister }>())
export const loadUser = createAction("[User info] Load info", props < {jwt:string}>());
export const setUser = createAction("[User info] set info", props<{ user: User }>());
export const resetUser = createAction("[User info] reset info");

