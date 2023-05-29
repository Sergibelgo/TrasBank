import { User } from "../User/user";

export interface UserState {
  loading: boolean,
  userJWT: string,
  user: User | null,
  errorMsg: string,
  index: number
}
