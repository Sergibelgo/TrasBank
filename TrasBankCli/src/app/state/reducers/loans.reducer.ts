import { createReducer, on } from '@ngrx/store';
import { resetUtils, setError, setIndex, setLoad, setSuccess } from '../actions/utils.actions';
import { LoanState } from '../../Models/loansState/loan-state';
import { addLoan, setActiveLoan, setLoans, setScoring } from '../actions/loans.actions';
import { setActive } from '../actions/accounts.actions';
import { Loan } from '../../Models/loan/loan';




export const initialState: LoanState = {
  loans: [],
  activeLoan: {
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
  },
  scoringForm: {
    Ammount: 500,
    Deposit: 0,
    Expenses: [],
    LoanTypeId: 1,
    Name: "",
    TIN_TAE: 1,
    TotalInstallments:1
  }
};

export const loansReducer = createReducer(
  initialState,
  on(setLoans, (state, action) => {
    return { ...state, loans: action.loans }
  }),
  on(setScoring, (state, action) => {
    return {
      ...state, scoringForm: action.scoring 
    }
  }),
  on(addLoan, (state, action) => {
    return { ...state, loans: [...state.loans, {...action.loan}] }
  }),
  on(setActiveLoan, (state, action) => {
    let active = state.loans.find(val => val.Id == action.id);
    return { ...state, activeLoan: {...active as Loan}}
  })
);
