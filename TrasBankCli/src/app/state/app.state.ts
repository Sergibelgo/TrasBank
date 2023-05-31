import { ActionReducerMap } from "@ngrx/store";
import { UserState } from "../Models/userState/user-state";
import { userReducer } from "./reducers/user.reducer";
import { Enumsstate } from "../Models/enumsState/enumsstate";
import { enumsReducer } from "./reducers/enums.reducer";
import { WorkerState } from "../Models/WorkerState/worker-state";
import { workerReducer } from "./reducers/worker.reducer";
import { AccountState } from "../Models/accountState/account-state";
import { accountsReducer } from "./reducers/accounts.reducer";
import { UtilsState } from "../Models/utilsStatus/utils-state";
import { utilsReducer } from "./reducers/utils.reducer";

export interface AppState {
  user: UserState,
  enums: Enumsstate,
  workers: WorkerState,
  accounts: AccountState,
  utils: UtilsState
}

export const ROOT_REDUCERS: ActionReducerMap<AppState> = {
  user: userReducer,
  enums: enumsReducer,
  workers: workerReducer,
  accounts: accountsReducer,
  utils: utilsReducer
}
