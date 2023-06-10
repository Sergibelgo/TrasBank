import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { LoanType } from '../../../../../Models/enumsState/enumsstate';
import { Subscription } from 'rxjs';
import { selectLoanTypes } from '../../../../../state/selectors/enums.selectors';
import { loadLoanTypes } from '../../../../../state/actions/enums.actions';
import { Scoring } from '../../../../../Models/scoring/scoring';
import { addLoan, setScoring } from '../../../../../state/actions/loans.actions';
import { LoansService } from '../../../../../services/loans.service';
import { selectJWT } from '../../../../../state/selectors/user.selectors';
import { errorAlert, successAlert } from '../../../../Utils';
import { setIndex, setLoad } from '../../../../../state/actions/utils.actions';
import { TranslateService } from '@ngx-translate/core';

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
    LoanTypeId: 1,
    Name: "",
    TIN_TAE: 1,
    TotalInstallments: 2
  }
  scoringAproved: Scoring | null = null;
  expDes: string = "";
  expSpend: number = 0;
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  constructor(private store: Store<any>,private service:LoansService,private trans:TranslateService) {
  }
  ngOnInit() {
    this.loanTypes$ = this.store.select(selectLoanTypes).subscribe(val => this.loanTypes = val);
   ​this.jwt$=this.store.select(selectJWT).subscribe(val=>this.jwt=val)
  }
  ngAfterViewInit() {
    if (this.loanTypes.length == 0) {
      this.store.dispatch(loadLoanTypes())
    }
  }
  ngOnDestroy() {
    this.loanTypes$.unsubscribe()
  }
  resetValues() {
    this.loanScoring.Deposit = 0;
  }
  updateScoringState() {
    this.store.dispatch(setScoring({ scoring: { ...this.loanScoring, Expenses: { ...this.loanScoring.Expenses } } }))
  }
  async submit() {
    if (this.isValidScoring()) {
      this.store.dispatch(setLoad({ load: true }));
      try {
        let result = await this.service.checkScoring(this.jwt, this.loanScoring)
        if (result) {
          successAlert(this.trans.instant("The loan can be requested, please press the button to confirm the loan"))
          $("input").attr("readonly", "true")
          $("select").attr("disabled","true")
          this.scoringAproved = this.loanScoring;
        } else {
          errorAlert(this.trans.instant("The loan cant be requested with the current options"))
        }
      } catch (err: any) {
        errorAlert(err);
      } finally {
        this.store.dispatch(setLoad({load:false}))
      }
    }
  }
  async request() {
    if (this.scoringAproved != null) {
      this.store.dispatch(setLoad({load:true}))
      try {
        let result = await this.service.requestScoring(this.jwt, this.scoringAproved);
        this.store.dispatch(addLoan({ loan: { ...result } }))
        successAlert(this.trans.instant("The loan was requested successfully, wait for our employe to get in contact with you"))
        this.store.dispatch(setIndex({index:0}))
      } catch (err: any) {
        errorAlert(err)
      } finally {
        this.store.dispatch(setLoad({load:false}))
      }
      
    }
  }
  isValidScoring(): boolean{
    if (this.expSpend != 0 && this.expSpend != undefined) {
      if (this.expSpend < 0) {
        return false;
      }
      this.loanScoring.Expenses[0] = { Description: this.expDes, Spend: this.expSpend }
    } else {
      this.loanScoring.Expenses[0] = {Description:"None",Spend:0};
    }
    if (this.loanScoring.Name == "") {
      return false
    }
    if (this.loanScoring.Ammount < 1) {
      this.loanScoring.Ammount = 500;
      return false
    }
    if (this.loanScoring.TotalInstallments < 1) {
      this.loanScoring.TotalInstallments=1
      return false;
    }
    if (this.loanScoring.Deposit < 0) {
      this.loanScoring.Deposit = 0
      return false
    }
    if (this.loanScoring.TIN_TAE != 1 && this.loanScoring.TIN_TAE!=2) {
      this.loanScoring.TIN_TAE = 1;
      return false;
    }
    
    return true;
  }
}
