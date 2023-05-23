import { Transaction } from "../Transaction/transaction";

export class Account {
  name: string;
  accountType: string;
  accountStatus: string;
  saveDate: Date;
  balance: number;
  transactions: Transaction[];
  constructor(name: string, accountT: string, accountStatus: string, saveDate: Date, balance: number, transactions: Transaction[]) {
    this.name = name;
    this.accountType = accountT;
    this.accountStatus = accountStatus;
    this.saveDate = saveDate;
    this.balance = balance;
    this.transactions = transactions;
  }
}
