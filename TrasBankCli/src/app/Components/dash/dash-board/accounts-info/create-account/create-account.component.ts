import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AccountDTO } from '../../../../../Models/createAccount/account-dto';
import { selectAccountTypes } from '../../../../../state/selectors/enums.selectors';
import { Enum } from '../../../../../Models/enumsState/enumsstate';
import { loadAccountTypes } from '../../../../../state/actions/enums.actions';
import { tryCreateAccount } from '../../../../../state/actions/accounts.actions';
import { selectJWT } from '../../../../../state/selectors/user.selectors';
import { setLoad } from '../../../../../state/actions/utils.actions';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit, OnDestroy {
  frmCreateAccount: FormGroup;
  accountTypes$: Subscription;
  accountTypes: Enum[] = [];
  jwt$: Subscription;
  jwt: string = "";
  today: string = this.getToday();
  newAccount: AccountDTO = { accountName: "", accountType: 1, saveUntil: new Date() };


  constructor(private store: Store<any>, private frmBuilder: FormBuilder) {
    this.accountTypes$ = this.store.select(selectAccountTypes).pipe(val => val).subscribe(val => this.accountTypes = val)
    this.jwt$ = this.store.select(selectJWT).pipe(val => val).subscribe(val => this.jwt = val);
    this.frmCreateAccount = this.frmBuilder.group({
      accountName: ["", Validators.required],
      accountType: ["", Validators.required],
      saveUntil: [this.today, Validators.required]
    })
  }
  ngOnDestroy(): void {
    this.accountTypes$.unsubscribe();
  }
  ngOnInit(): void {
    if (this.accountTypes.length == 0) {
      this.store.dispatch(loadAccountTypes())
    }
  }
  submit() {
    if (this.frmCreateAccount.valid) {
      this.store.dispatch(setLoad({ load: true }))
      this.store.dispatch(tryCreateAccount({ account: { ...this.newAccount }, jwt: this.jwt }));
    }
  }
  getToday() {
    let date = new Date();
    let day = date.getDay();
    let month = date.getMonth();
    let year = date.getFullYear();
    let today = `${year}-${month}-${day}`;
    return today;
  }
}
