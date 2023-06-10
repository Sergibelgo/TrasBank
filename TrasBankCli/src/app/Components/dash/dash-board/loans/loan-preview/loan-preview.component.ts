import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Scoring } from '../../../../../Models/scoring/scoring';
import { Store } from '@ngrx/store';
import { selectScoring } from '../../../../../state/selectors/loans.selectors';
import { LoanType } from '../../../../../Models/enumsState/enumsstate';
import { selectLoanTypes } from '../../../../../state/selectors/enums.selectors';

@Component({
  selector: 'app-loan-preview',
  templateUrl: './loan-preview.component.html',
  styleUrls: ['./loan-preview.component.css']
})
export class LoanPreviewComponent implements OnInit, OnDestroy, AfterViewInit {
  scoring$: Subscription = new Subscription();
  scoring: Scoring = {
    Ammount: 500,
    Deposit: 0,
    Expenses: [],
    LoanTypeId: 1,
    Name: "",
    TIN_TAE: 1,
    TotalInstallments:2
  }
  loanTypes$: Subscription = new Subscription();
  loanTypes: LoanType[] = []
  totalAmmount: number = 0
  percentage: number = 0;
  constructor(private store: Store<any>) {

  }
  ngAfterViewInit(): void {
    }
  ngOnDestroy(): void {
    this.scoring$.unsubscribe()
    }
  ngOnInit(): void {
    this.scoring$ = this.store.select(selectScoring).subscribe(val => { this.scoring = val; this.updateTotal() });
    this.loanTypes$ = this.store.select(selectLoanTypes).subscribe(val => this.loanTypes = val)
  }
  updateTotal() {
    if (this.loanTypes.length > 0) {
      let loanType: LoanType = this.loanTypes.find(val => val.Id == this.scoring.LoanTypeId) as LoanType
      this.percentage = this.scoring.TIN_TAE == 1 ? loanType.TIN : loanType.TAE
      this.totalAmmount = this.scoring.Ammount + (this.scoring.Ammount * (this.percentage) / 100)
    }

  }
}
