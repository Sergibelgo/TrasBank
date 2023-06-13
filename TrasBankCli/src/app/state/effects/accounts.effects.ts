import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY, of } from 'rxjs';
import { map, exhaustMap, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { AccountsService } from '../../services/accounts.service';
import { TransactionsService } from '../../services/transactions.service';

@Injectable()
export class AccountsEffects {
  accountsList$ = createEffect(() => this.actions$.pipe(
    ofType("[Accounts] Load accounts"),
    mergeMap((action: any) => this.accountsService.getAccounts(action.jwt)
      .then((accounts) => ({ type: "[Accounts] Set accounts", accounts }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  )
  );
  transactionsList$ = createEffect(() => this.actions$.pipe(
    ofType("[Transactions] load transactions"),
    mergeMap((action: any) => this.transactionsService.getTransactions(action.jwt, action.accountId)
      .then((transactions) => ({ type: "[Transactions] set transactions", transactions }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  createAccount$ = createEffect(() => this.actions$.pipe(
    ofType("[Accounts] Create new"),
    mergeMap((action: any) => this.accountsService.createAccount(action.jwt, action.account)
      .then((account) => ({ type: "[Accounts] add account" ,account}))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  loadAccountsByCustomerId$ = createEffect(() => this.actions$.pipe(
    ofType("[Accounts] Load accounts by user id"),
    mergeMap((action: any) => this.accountsService.getByCustomerId(action.jwt, action.id)
      .then((accounts) => ({ type: "[Accounts] Set accounts", accounts }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  loadTransactionsByCustomerId$ = createEffect(() => this.actions$.pipe(
    ofType("[Transactions] Load transactions by user id"),
    mergeMap((action: any) => this.transactionsService.getByCustomerId(action.jwt, action.id)
      .then((transactions) => ({ type: "[Transactions] set transactions", transactions }))
      .catch((error) => ({ type: "[Errors] Set Error", error }))
    )
  ))
  constructor(
    private actions$: Actions,
    private accountsService: AccountsService,
    private transactionsService: TransactionsService
  ) { }
}
