import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Loan } from '../../../../../Models/loan/loan';
import { Store } from '@ngrx/store';
import { selectActiveLoan } from '../../../../../state/selectors/loans.selectors';
import { PaymentDTO } from '../../../../../Models/paymentDTO/payment-dto';
import { Account } from '../../../../../Models/Account/account';
import { selectAccounts } from '../../../../../state/selectors/accounts.selectors';
import { PaymentsService } from '../../../../../services/payments.service';
import { errorAlert, successAlert } from '../../../../Utils';
import { TranslateService } from '@ngx-translate/core';
import Swal from 'sweetalert2';
import { loadLoans } from '../../../../../state/actions/loans.actions';
import { selectJWT } from '../../../../../state/selectors/user.selectors';
import { loadAccounts } from '../../../../../state/actions/accounts.actions';
declare var $: any;

@Component({
  selector: 'app-show-card',
  templateUrl: './show-card.component.html',
  styleUrls: ['./show-card.component.css']
})
export class ShowCardComponent implements OnInit, OnDestroy {
  actieLoan$: Subscription = new Subscription();
  activeLoan: Loan = {
    Ammount: 0,
    CustomerId: "",
    CustomerName: "",
    EndDate: new Date(),
    Id: "",
    InterestRate: 0,
    LoanStatus: "",
    LoanType: "",
    RemainingAmmount: 0,
    RemainingInstallments: 0,
    StartDate: new Date(),
    TotalInstallments: 0,
    LoanName: ""
  }
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  accounts$: Subscription = new Subscription();
  accountsBasicInfo: { Id: string, Name: string }[] = [];
  payment: PaymentDTO = {
    AccountId: "",
    LoanId: "",
    NumberInstallments: 1
  }
  constructor(private store: Store<any>, private service: PaymentsService, private trans: TranslateService) {

  }
  ngOnDestroy(): void {
    this.actieLoan$.unsubscribe()
    this.accounts$.unsubscribe();
    this.jwt$.unsubscribe();
  }
  ngOnInit(): void {
    this.actieLoan$ = this.store.select(selectActiveLoan).subscribe(val => { this.activeLoan = val; this.payment.LoanId = val.Id })
    this.accounts$ = this.store.select(selectAccounts).subscribe(val => this.accountsBasicInfo = val.map((item) => {
      return { Id: item.Id, Name: item.Name }
    }));
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val);
  }
  makePayment() {
    if (this.payment.AccountId != "" && this.payment.AccountId != "" && this.payment.NumberInstallments > 0) {
      Swal.fire({
        icon: "info",
        text: this.trans.instant("Are you sure?"),
        showCancelButton: true
      }).then((val) => {
        if (val.isConfirmed) {
          this.service.makePayment(this.jwt, this.payment).then(
            val => {
              successAlert(this.trans.instant("Payment done"));
              this.store.dispatch(loadLoans({ jwt: this.jwt }))
              this.store.dispatch(loadAccounts({ jwt: this.jwt }));
              $("#ModalActiveLoan").modal("hide")
            }
          ).catch(
            val => errorAlert(val)
          );
        }
      })

    }
  }
}
