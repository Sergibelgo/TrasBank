import { createAction, createActionGroup, props } from '@ngrx/store';
import { User } from '../../Models/User/user';
import { WorkerPublicInfo } from '../../Models/Worker/worker';

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
export const loadPublicInfo = createAction("[Worker] load public info");
export const setPublicInfo = createAction("[Worker] Set public info", props<{ workersPublicInfo: WorkerPublicInfo[] }>());
