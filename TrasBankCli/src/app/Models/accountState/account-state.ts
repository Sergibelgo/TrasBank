import { Account } from "../Account/account";
import { Transaction } from "../Transaction/transaction";

export interface AccountState {
  active: Account;
  accounts: Account[];
  transactions: Transaction[];
}
