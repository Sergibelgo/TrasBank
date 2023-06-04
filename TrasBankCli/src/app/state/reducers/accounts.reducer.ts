import { createReducer, on } from '@ngrx/store';
import { AccountState } from '../../Models/accountState/account-state';
import { addAccount, resetAccounts, setAccounts, setActive, setAll, setTransactions, updateAccountsName } from '../actions/accounts.actions';
import { Account } from '../../Models/Account/account';
import { aC } from '@fullcalendar/core/internal-common';

const generateAll = (accounts: Account[]) => {
  let balance = 0;
  accounts.forEach(item => {
    balance = balance + item.Balance
  })
  let account: Account = {
    Name: "all",
    Balance: balance,
    Status: "Enabled",
    Type: "Normal",
    SaveUntil: new Date(),
    Id: ""
  }
  return account;
}


export const initialState: AccountState = {
  active: { Balance: 0, Name: "", SaveUntil: new Date(), Status: "", Type: "", Id: "" },
  accounts: [],
  transactions: []
};

export const accountsReducer = createReducer(
  initialState,
  on(setAccounts, (state, action) => {
    if (state.active.Name == "") {
      let active = generateAll(action.accounts);
      return { ...state, accounts: action.accounts, active: active }
    }
    return { ...state, accounts: action.accounts }
  }),
  on(setActive, (state, action) => {
    return { ...state, active: action.account }
  }),
  on(setTransactions, (state, action) => {
    return { ...state, transactions: action.transactions }
  }),
  on(setAll, (state, action) => {
    let active = generateAll(action.accounts);
    return { ...state, active: active }
  }),
  on(resetAccounts, (state, action) => {
    return initialState;
  }),
  on(addAccount, (state, action) => {
    let accounts = [...state.accounts];
    accounts.push(action.account);
    return { ...state, accounts: accounts}
  }),
  on(updateAccountsName, (state, action) => {
    let accounts = [...state.accounts];
    let index = accounts.findIndex(item => item.Id == action.account.Id)
    accounts[index] = { ...accounts[index], Name: action.account.Name }
    return { ...state, accounts: accounts }
  })

);

