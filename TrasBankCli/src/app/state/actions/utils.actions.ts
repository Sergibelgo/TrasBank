import { createAction, createActionGroup, props } from '@ngrx/store';


export const setLoad = createAction("[Utils] Load", props<{ load: boolean }>());
export const setError = createAction("[Errors] Set Error", props<{ error: string }>());
export const setIndex = createAction("[Dash]set index", props<{ index: number }>());
export const setSuccess = createAction("[Dash]set success", props<{ msg: string }>());
export const resetUtils = createAction("[Dash]reset");
