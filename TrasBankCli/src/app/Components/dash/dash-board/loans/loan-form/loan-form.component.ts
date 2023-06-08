import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { LoanType } from '../../../../../Models/enumsState/enumsstate';
import { Subscription } from 'rxjs';
import { selectLoanTypes } from '../../../../../state/selectors/enums.selectors';
import { loadLoanTypes } from '../../../../../state/actions/enums.actions';
import { Scoring } from '../../../../../Models/scoring/scoring';

@Component({
  selector: 'app-loan-form',
  templateUrl: './loan-form.component.html',
  styleUrls: ['./loan-form.component.css']
})
export class LoanFormComponent implements OnInit, OnDestroy, AfterViewInit {
  loanTypes$: Subscription = new Subscription();
  loanTypes: LoanType[] = [];
  loanScoring: Scoring = {
    Ammount: 500,
    Deposit: 0,
    Expenses: [],
    LoanType: 3,
    Name: "",
    TIN_TAE: 1,
    TotalInstallments: 1
  }
  constructor(private store: Store<any>) {

  }
  ngOnInit() {
    this.loanTypes$ = this.store.select(selectLoanTypes).subscribe(val => this.loanTypes = val);
   ​
  }
  ngAfterViewInit() {
    if (this.loanTypes.length == 0) {
      this.store.dispatch(loadLoanTypes())
    }
  }
  ngOnDestroy() {
    this.loanTypes$.unsubscribe()
  }

  addExpense() {
    if (this.loanScoring.Expenses.length < 5) {
      this.loanScoring.Expenses.push({ Description: "", Spend: 0 })
    }
  }
  removeExpense() {
    this.loanScoring.Expenses.pop()
  }
  resetValues() {
    this.loanScoring.Expenses = [];
    this.loanScoring.Deposit = 0;
  }
}
