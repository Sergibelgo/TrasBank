import { createReducer, on } from '@ngrx/store';
import { resetUser, setError, setIndex, setLoad, setUser, setUserJWT } from '../actions/auth.actions';
import { AccountState } from '../../Models/accountState/account-state';
import { setAccounts, setActive, setAll, setTransactions } from '../actions/accounts.actions';
import { Account } from '../../Models/Account/account';

const generateAll = (accounts: Account[]) => {
  let balance = 0;
  accounts.forEach(item => {
    balance = balance + item.Balance
  })
  let account: Account = {
    Name: "all",
    Balance: balance,
    Status: "Active",
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
  })

);

