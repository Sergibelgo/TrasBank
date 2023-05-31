import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AccountDTO } from '../../../../Models/createAccount/account-dto';
import { selectAccounts } from '../../../../state/selectors/accounts.selectors';
import { url } from '../../../../services/base-service.service';
import { selectJWT } from '../../../../state/selectors/user.selectors';
import { TransactionsService } from '../../../../services/transactions.service';
import { setError, setSuccess } from '../../../../state/actions/utils.actions';
import { toast } from '../../../Utils';
import { TranslateService } from '@ngx-translate/core';
declare var $: any;

@Component({
  selector: 'app-transactions-make',
  templateUrl: './transactions-make.component.html',
  styleUrls: ['./transactions-make.component.css']
})
export class TransactionsMakeComponent {
  accounts$: Subscription;
  accounts: { Id: string, Name: string }[] = [];
  jwt$: Subscription;
  jwt: string = "";
  frmTransaction: FormGroup;
  otherUser: string = "";
  otherAccounts: {    Id: string, AccountName: string }[] = [];
  constructor(private store: Store<any>, private frmBuilder: FormBuilder, private service: TransactionsService, private translate: TranslateService) {
    this.frmTransaction = this.frmBuilder.group({
      username: new FormControl('', [Validators.required]),
      account: new FormControl("", [Validators.required])
    })
    this.accounts$ = this.store.select(selectAccounts).pipe(val => val)
      .subscribe(val => this.accounts = val.map((val) => { return { Id: val.Id, Name: val.Name } }))
    this.jwt$ = this.store.select(selectJWT).pipe(val => val).subscribe(val => this.jwt=val)

  }
  clearOtherAccounts() {
    this.otherAccounts = [];
  }
  async getUserAccounts() {
    $("#check").attr("disabled", true);
    try {
      this.otherAccounts = await this.service.getUserAccounts(this.jwt, this.otherUser);
      console.log(this.otherAccounts);
      toast(this.translate.instant("User found"),"success")
    } catch {
      toast(this.translate.instant("User not found"), "error")
    } finally {
      $("#check").attr("disabled", false);
    }
  }
}
