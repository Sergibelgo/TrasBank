import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import listPlugin from '@fullcalendar/list';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { selectAccountsInfo, selectJWT, selectUser } from '../../../../state/selectors/user.selectors';
import { Account } from '../../../../Models/Account/account';
import { selectAccountActive, selectAccounts, selectTransactions } from '../../../../state/selectors/accounts.selectors';
import { loadAccounts, loadTransactions, setActive, setAll } from '../../../../state/actions/accounts.actions';
import { Transaction } from '../../../../Models/Transaction/transaction';
import { FullCalendarComponent } from '@fullcalendar/angular';

@Component({
  selector: 'app-accounts-info',
  templateUrl: './accounts-info.component.html',
  styleUrls: ['./accounts-info.component.css']
})
export class AccountsInfoComponent implements OnInit, OnDestroy {
  @ViewChild('calendar') calendarComponent: FullCalendarComponent | undefined;
  jwt$: Subscription;
  jwt: string = "";
  userInfo$: Subscription;
  accountsBasicInfo?: { Id: string, Name: string }[];
  accountActive$: Subscription;
  accountActive?: Account;
  accounts$: Subscription;
  accounts?: Account[];
  transactions$: Subscription;
  transactions: Transaction[] = [];
  calendarOptions: CalendarOptions = {
    //initialView: 'dayGridMonth',
    //plugins: [dayGridPlugin],
    initialView: 'dayGridMonth',
    plugins: [listPlugin, dayGridPlugin],
    editable: true
  };
  
  constructor(private store: Store<any>) {
    this.userInfo$ = this.store.select(selectUser).pipe(val => val).subscribe(val => this.accountsBasicInfo = val?.Accounts);
    this.accounts$ = this.store.select(selectAccounts).pipe(val => val).subscribe(val => this.accounts = val);
    this.accountActive$ = this.store.select(selectAccountActive).pipe(val => val).subscribe(val => this.accountActive = val);
    this.jwt$ = this.store.select(selectJWT).pipe(val => val).subscribe(val => this.jwt = val ?? "");
    this.transactions$ = this.store.select(selectTransactions).pipe(val => val).subscribe(val => this.transactions = val);
  }
  ngOnDestroy(): void {
    this.accountActive$.unsubscribe();
    this.userInfo$.unsubscribe();
    this.accounts$.unsubscribe();
    this.transactions$.unsubscribe();
  }
  ngOnInit(): void {
    this.store.dispatch(loadAccounts({ jwt: this.jwt }));
    this.store.dispatch(loadTransactions({ jwt: this.jwt, accountId: "" }))
  }

}
