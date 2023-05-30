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
import { TranslateService } from '@ngx-translate/core';
declare var $: any;

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
  accountActive: Account = { Balance: 0, Id: "", Name: "", SaveUntil: new Date(), Status:"",Type:"" }
;
  accounts$: Subscription;
  accounts?: Account[];
  transactions$: Subscription;
  transactions: Transaction[] = [];
  calendarOptions: CalendarOptions = {
    initialView: 'listMonth',
    plugins: [listPlugin, dayGridPlugin],
    locale: this.translateService.currentLang,
    editable: true,
    themeSystem: "bootstrap5",
    eventClick: (info) => this.showTransactionInfo(info)
  };
  modalTransaction: Transaction = { Ammount: 0, Concept: "", Date: new Date(), Id: "", NameOther: "", TipeTransaction: "" }

  constructor(private store: Store<any>, private translateService: TranslateService) {
    this.userInfo$ = this.store.select(selectUser).pipe(val => val).subscribe(val => this.accountsBasicInfo = val?.Accounts);
    this.accounts$ = this.store.select(selectAccounts).pipe(val => val).subscribe(val => this.accounts = val);
    this.accountActive$ = this.store.select(selectAccountActive).pipe(val => val).subscribe(val => this.accountActive = val);
    this.jwt$ = this.store.select(selectJWT).pipe(val => val).subscribe(val => this.jwt = val ?? "");
    this.transactions$ = this.store.select(selectTransactions).pipe(val => val).subscribe(val => { this.transactions = val; this.loadEvents(val) });
  }
  ngOnDestroy(): void {
    this.accountActive$.unsubscribe();
    this.userInfo$.unsubscribe();
    this.accounts$.unsubscribe();
    this.transactions$.unsubscribe();
    this.jwt$.unsubscribe();
  }
  ngOnInit(): void {
    this.store.dispatch(loadAccounts({ jwt: this.jwt }));
    this.store.dispatch(loadTransactions({ jwt: this.jwt, accountId: "" }));
  }
  actTransactions(event: Event) {
    let id = $(event.target).val();
    let account = (this.accounts as Account[]).find(account => account.Id == id);
    if (account != undefined) {
      this.store.dispatch(loadTransactions({ jwt: this.jwt, accountId: id }));
      this.store.dispatch(setActive({ account: account }))
    } else {
      this.store.dispatch(loadTransactions({ jwt: this.jwt, accountId: "" }));
      this.store.dispatch(setAll({ accounts: this.accounts as Account[] }));
    }
  }
  transFormTranToEvent(transactions: Transaction[]) {
    let events = transactions.map(item => {
      let color;
      if (item.TipeTransaction == "Add" || item.TipeTransaction == "Loan Approved") {
        color = "green";
      } else if (item.TipeTransaction == "Draw") {
        color = "red";
      } else {
        color = "purple"
      }
      return { title: item.Concept ?? this.translateService.instant(item.TipeTransaction), date: item.Date, backgroundColor: color, id: item.Id, display: "auto", Type: this.translateService.instant(item.TipeTransaction), Other: item.NameOther, Ammount: item.Ammount }
    });
    return events;
  }
  loadEvents(val: Transaction[]) {
    let events = this.transFormTranToEvent(val);
    this.calendarOptions.events = events;
    this.calendarOptions.locale = this.translateService.currentLang;
  }
  showTransactionInfo(info: any) {
    let trans: Transaction = {
      Ammount: info.event.extendedProps.Ammount,
      Concept: info.event.title,
      Date: info.event.start,
      Id: info.event.id,
      NameOther: info.event.extendedProps.Other,
      TipeTransaction: info.event.extendedProps.Type
    }
    this.modalTransaction = trans;
    $("#Modal").modal("show");
  }
}
