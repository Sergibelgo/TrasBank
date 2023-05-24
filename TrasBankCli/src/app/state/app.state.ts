import { ActionReducerMap } from "@ngrx/store";
import { UserState } from "../Models/userState/user-state";
import { userReducer } from "./reducers/user.reducer";
import { Enumsstate } from "../Models/enumsState/enumsstate";
import { enumsReducer } from "./reducers/enums.reducer";

export interface AppState {
  user: UserState,
  enums: Enumsstate
}

export const ROOT_REDUCERS: ActionReducerMap<AppState> = {
  user: userReducer,
  enums: enumsReducer
}
