import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Loan } from '../../../../../Models/loan/loan';
import { Store } from '@ngrx/store';
import { selectActiveLoan } from '../../../../../state/selectors/loans.selectors';

@Component({
  selector: 'app-show-card',
  templateUrl: './show-card.component.html',
  styleUrls: ['./show-card.component.css']
})
export class ShowCardComponent implements OnInit,OnDestroy {
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
    LoanName:""
  }
  constructor(private store: Store<any>) {

  }
    ngOnDestroy(): void {
        this.actieLoan$.unsubscribe()
    }
    ngOnInit(): void {
        this.actieLoan$=this.store.select(selectActiveLoan).subscribe(val=>this.activeLoan=val)
    }
}
