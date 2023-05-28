import { Transaction } from "../Transaction/transaction";

export interface Account {
  name: string;
  accountType: string;
  accountStatus: string;
  saveDate: Date;
  balance: number;
  transactions: Transaction[];
}
