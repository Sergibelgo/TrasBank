import { AfterViewInit, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { Enum } from '../../../../../../Models/enumsState/enumsstate';
import { selectAccountStatuses } from '../../../../../../state/selectors/enums.selectors';
import { loadAccountStatuses } from '../../../../../../state/actions/enums.actions';
import Swal from 'sweetalert2';
import { TranslateService } from '@ngx-translate/core';
import { AccountsService } from '../../../../../../services/accounts.service';
import { selectJWT } from '../../../../../../state/selectors/user.selectors';
import { errorAlert, successAlert } from '../../../../../Utils';
import { setLoad } from '../../../../../../state/actions/utils.actions';
import { loadAccounts, loadAccountsByUserId } from '../../../../../../state/actions/accounts.actions';
import { selectActiveUser } from '../../../../../../state/selectors/worker.selectors';
import { TransactionsService } from '../../../../../../services/transactions.service';
declare var $: any;
@Component({
  selector: 'app-modal-edit-account-worker',
  templateUrl: './modal-edit-account-worker.component.html',
  styleUrls: ['./modal-edit-account-worker.component.css']
})
export class ModalEditAccountWorkerComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() option: boolean = true;
  @Input() id: string = "";
  accountStatuses$: Subscription = new Subscription();
  accountStatuses: Enum[] = [];
  quantity: number = 5;
  concept: string = "";
  status: number = 1;
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  user$: Subscription = new Subscription();
  user: string = "";
  constructor(private store: Store<any>, private trans: TranslateService, private serviceAc: AccountsService, private serviceTra: TransactionsService) {

  }

  ngOnDestroy(): void {
    this.accountStatuses$.unsubscribe();
    this.jwt$.unsubscribe()
    this.user$.unsubscribe();
  }
  ngOnInit(): void {
    this.accountStatuses$ = this.store.select(selectAccountStatuses).subscribe(val => this.accountStatuses = val);
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val);
    this.user$ = this.store.select(selectActiveUser).subscribe(val => this.user = val);
  }
  ngAfterViewInit(): void {
    if (this.accountStatuses.length == 0) {
      this.store.dispatch(loadAccountStatuses())
    }
  }
  submit() {
    if (this.quantity != 0) {
      Swal.fire({
        icon: "info",
        showCancelButton: true,
        text: this.trans.instant("Are you sure of the operation") + "?"
      }).then((val) => {
        if (val.isConfirmed) {
          if (this.option) {
            this.editAccount()
          } else {
            this.transferMoney()
          }
        }
      })
    }
  }
  editAccount() {
    this.store.dispatch(setLoad({ load: true }));
    this.serviceAc.updateAccountStatus(this.jwt, this.id, this.status).then(val => {
      successAlert(this.trans.instant("Account updated successfully"))
      this.store.dispatch(loadAccountsByUserId({ jwt: this.jwt, id: this.user }))
    }).catch(val => {
      errorAlert(val)
    }).finally(() => {
      this.store.dispatch(setLoad({ load: false }))
      $(".modal").modal("hide")
      this.resetForm();
    })
  }
   
  transferMoney() {

    this.store.dispatch(setLoad({ load: true }));
    this.serviceTra.AddorRemoveMoney(this.jwt, this.id, { concept: this.concept, quantity: this.quantity }).then((val) => {
      successAlert(this.trans.instant("Account updated successfully"))
      this.store.dispatch(loadAccountsByUserId({ jwt: this.jwt, id: this.user }))
    }).catch(val => {
      errorAlert(val)
    }).finally(() => {
      this.store.dispatch(setLoad({ load: false }))
      $(".modal").modal("hide")
      this.resetForm();
    })
  }
  resetForm() {
    this.quantity = 5;
    this.concept = "";
  }
}
