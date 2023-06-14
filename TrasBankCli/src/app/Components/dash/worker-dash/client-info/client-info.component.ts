import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { User } from '../../../../Models/User/user';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { selectActiveUser, selectCustomers } from '../../../../state/selectors/worker.selectors';
import { loadAccountsByUserId, loadTransactionsByUserId } from '../../../../state/actions/accounts.actions';
import { selectJWT } from '../../../../state/selectors/user.selectors';
import { loadLoansByUserId } from '../../../../state/actions/loans.actions';
import { setIndex } from '../../../../state/actions/utils.actions';
import { setUser } from '../../../../state/actions/auth.actions';

@Component({
  selector: 'app-client-info',
  templateUrl: './client-info.component.html',
  styleUrls: ['./client-info.component.css']
})
export class ClientInfoComponent implements OnInit, OnDestroy, AfterViewInit  {
  activeUser$: Subscription = new Subscription();
  activeUser: string = "";
  customers$: Subscription = new Subscription();
  customers: User[] = [];
  jwt$: Subscription = new Subscription();
  jwt:string=""
  constructor(private store: Store<any>) {

  }
    
  ngOnDestroy(): void {
    this.activeUser$.unsubscribe();
    this.jwt$.unsubscribe();
    this.customers$.unsubscribe();
    }
  ngOnInit(): void {
    this.activeUser$ = this.store.select(selectActiveUser).subscribe(val => this.activeUser = val);
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val)
    this.customers$ = this.store.select(selectCustomers).subscribe(val=>this.customers=val)
  }
  ngAfterViewInit(): void {
    if (this.activeUser != "") {
      this.store.dispatch(loadAccountsByUserId({ id: this.activeUser, jwt: this.jwt }))
      this.store.dispatch(loadTransactionsByUserId({ id: this.activeUser, jwt: this.jwt }))
      this.store.dispatch(loadLoansByUserId({ id: this.activeUser, jwt: this.jwt }))
    } else {
      this.store.dispatch(setIndex({ index: 0 }));
    }
  }

}
