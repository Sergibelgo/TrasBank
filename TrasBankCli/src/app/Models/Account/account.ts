import { Transaction } from "../Transaction/transaction";

export interface Account {
  Id: string;
  Name: string;
  Type: string;
  Status: string;
  SaveUntil: Date;
  Balance: number;
}
