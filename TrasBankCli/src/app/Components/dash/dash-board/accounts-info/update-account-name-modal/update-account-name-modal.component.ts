import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { selectAccountActive } from '../../../../../state/selectors/accounts.selectors';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Account } from '../../../../../Models/Account/account';
import { AccountsService } from '../../../../../services/accounts.service';
import { selectJWT } from '../../../../../state/selectors/user.selectors';
import { setLoad } from '../../../../../state/actions/utils.actions';
import { errorAlert, successAlert } from '../../../../Utils';
import { TranslateService } from '@ngx-translate/core';
import { loadTransactions, setActive, updateAccountsName } from '../../../../../state/actions/accounts.actions';
declare var $: any;

@Component({
  selector: 'app-update-account-name-modal',
  templateUrl: './update-account-name-modal.component.html',
  styleUrls: ['./update-account-name-modal.component.css']
})
export class UpdateAccountNameModalComponent implements OnInit, OnDestroy {
  activeAccount$: Subscription = new Subscription();
  activeAccount: Account = { Balance: 0, Id: "", Name: "", SaveUntil: new Date(), Status: "", Type: "" };
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  frmUpdate: FormGroup;
  constructor(private store: Store<any>, private frmBuilder: FormBuilder, private accountsService: AccountsService, private trans: TranslateService) {

    this.frmUpdate = this.frmBuilder.group({
      accountName: [this.activeAccount.Name, Validators.required]
    }
    )
  }
  ngOnDestroy(): void {
    this.activeAccount$.unsubscribe();
    this.jwt$.unsubscribe();
  }
  ngOnInit(): void {
    this.activeAccount$ = this.store.select(selectAccountActive).subscribe(val => this.activeAccount = val);
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val);
  }
  submit() {
    if (this.frmUpdate.valid && this.frmUpdate.value.accountName.trim()!="") {
      this.store.dispatch(setLoad({load:true}))
      this.accountsService.updateAccountName(this.frmUpdate.value.accountName, this.activeAccount.Id, this.jwt)
        .then(() => {
          successAlert(this.trans.instant("Account name updated"));
          let updated = { ...this.activeAccount, Name: this.frmUpdate.value.accountName };
          this.store.dispatch(setActive({ account: updated }));
          this.store.dispatch(updateAccountsName({ account: updated }));
          this.store.dispatch(loadTransactions({ jwt: this.jwt, accountId: "" }));
          $("#ModalUpdate").modal("hide");

        })
        .catch((err) => { errorAlert(err) })
        .finally(() => {
          this.store.dispatch(setLoad({ load: false }))
        })
    }
  }
}
