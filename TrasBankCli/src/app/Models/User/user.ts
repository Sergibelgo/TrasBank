import { Account } from "../Account/account";

export interface User {
  FirstName: string;
  LastName: string;
  Username: string;
  Email: string;
  Income: number;
  Accounts: { Id: string,Name:string }[];
  Age: Date;
  }

