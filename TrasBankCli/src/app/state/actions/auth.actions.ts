import { createAction, createActionGroup, props } from '@ngrx/store';
import { User } from '../../Models/User/user';
import { UserRegister } from '../../Models/UserRegister/user-register';

//export const BooksActions = createActionGroup({
//  source: 'Books',
//  events: {
//    'Add Book': props<{ bookId: string }>(),
//    'Remove Book': props<{ bookId: string }>(),
//  },
//});

//export const BooksApiActions = createActionGroup({
//  source: 'Books API',
//  events: {
//    'Retrieved Book List': props<{ books: ReadonlyArray<Book> }>(),
//  },
//});
export const setLoad = createAction("[User login] Load user", props<{ load: boolean }>());
export const setUserJWT = createAction("[User login] Set JWT", props<{ userJWT: string }>());
export const tryLogIn = createAction("[User login] Try login", props<{ password: string, username?: string, email?: string }>());
export const tryRegister = createAction("[User register] Try register", props<{ data: UserRegister }>())
export const setError = createAction("[Errors] Set Error", props<{ error: string }>());
export const loadUser = createAction("[User info] Load info", props < {jwt:string}>());
export const setUser = createAction("[User info] set info", props < {user:User}>())
