import { createAction, createActionGroup, props } from '@ngrx/store';
import { Account } from '../../Models/Account/account';
import { Transaction } from '../../Models/Transaction/transaction';
import { AccountDTO } from '../../Models/createAccount/account-dto';


export const loadAccounts = createAction("[Accounts] Load accounts", props<{ jwt: string }>());
export const setActive = createAction("[Accounts] Set active", props<{ account: Account }>())
export const setAccounts = createAction("[Accounts] Set accounts", props<{ accounts: Account[] }>())
export const setAll = createAction("[Accounts] Set active all", props<{ accounts: Account[] }>())
export const loadTransactions = createAction("[Transactions] load transactions", props<{ accountId: string, jwt: string }>());
export const setTransactions = createAction("[Transactions] set transactions", props<{ transactions: Transaction[] }>());
export const loadTransactionsByUserId = createAction("[Transactions] Load transactions by user id", props<{jwt:string,id:string}>())
export const tryCreateAccount = createAction("[Accounts] Create new", props<{ account: AccountDTO, jwt: string }>());
export const resetAccounts = createAction("[Accounts] reset accounts");
export const addAccount = createAction("[Accounts] add account", props<{ account: Account }>());
export const updateAccountsName = createAction("[Accounts] update account name", props<{ account: Account }>())
export const loadAccountsByUserId = createAction("[Accounts] Load accounts by user id", props<{jwt:string,id:string}>())

