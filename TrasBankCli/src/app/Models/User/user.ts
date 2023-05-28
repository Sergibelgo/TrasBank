import { Account } from "../Account/account";

export interface User {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  income: number;
  accounts: { id: string,name:string }[];
  age: Date;
  }

