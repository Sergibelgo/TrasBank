import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { selectAccounts } from '../../../../state/selectors/accounts.selectors';
import { selectJWT } from '../../../../state/selectors/user.selectors';
import { TransactionsService } from '../../../../services/transactions.service';
import { errorAlert, successAlert, toast } from '../../../Utils';
import { TranslateService } from '@ngx-translate/core';
import Swal from 'sweetalert2';
import { TransactionDto } from '../../../../Models/transactionDTO/transaction-dto';
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
  otherAccounts: { Id: string, AccountName: string }[] = [];
  constructor(private store: Store<any>, private frmBuilder: FormBuilder, private service: TransactionsService, private translate: TranslateService) {
    this.frmTransaction = this.frmBuilder.group({
      username: new FormControl('', [Validators.required]),
      account: new FormControl("", [Validators.required]),
      otherAccounts: new FormControl("", [Validators.required]),
      ammount: new FormControl("", [Validators.required]),
      concept: new FormControl("", [])
    })
    this.accounts$ = this.store.select(selectAccounts).pipe(val => val)
      .subscribe(val => this.accounts = val.map((val) => { return { Id: val.Id, Name: val.Name } }))
    this.jwt$ = this.store.select(selectJWT).pipe(val => val).subscribe(val => this.jwt = val)

  }
  clearOtherAccounts() {
    this.otherAccounts = [];
  }
  async getUserAccounts() {
    $("#check").attr("disabled", true);
    try {
      this.otherAccounts = await this.service.getUserAccounts(this.jwt, this.frmTransaction.value.username);
      toast(this.translate.instant("User found"), "success")
    } catch {
      toast(this.translate.instant("User not found"), "error")
    } finally {
      $("#check").attr("disabled", false);
    }
  }
  confirm() {
    if (this.frmTransaction.valid) {
      Swal.fire({
        icon: "info",
        showCancelButton: true,
        text: this.translate.instant("Are you sure of the transaction")+"?"
      }).then((res) => {
        if (res.isConfirmed) {
          let values = this.frmTransaction.value;
          let transaction: TransactionDto = { AccountReciverId: values.otherAccounts, AccountSenderId: values.account, Concept: values.concept, Quantity: values.ammount };
          this.service.makeTransaction(transaction, this.jwt).then(() => {
            successAlert(this.translate.instant("Transaction done"))
          }).catch((err) => {
            errorAlert(err);
          });

        }
      })
    }

  }
}
