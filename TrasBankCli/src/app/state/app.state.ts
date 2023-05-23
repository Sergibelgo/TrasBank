import { ActionReducerMap } from "@ngrx/store";
import { UserState } from "../Models/userState/user-state";
import { userReducer } from "./reducers/user.reducer";

export interface AppState {
  user: UserState
}

export const ROOT_REDUCERS: ActionReducerMap<AppState> = {
  user: userReducer
}
