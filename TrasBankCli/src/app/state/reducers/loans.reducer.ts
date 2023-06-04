import { createReducer, on } from '@ngrx/store';
import { resetUtils, setError, setIndex, setLoad, setSuccess } from '../actions/utils.actions';
import { LoanState } from '../../Models/loansState/loan-state';
import { setLoans } from '../actions/loans.actions';




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
    TotalInstallments: 0
  }
};

export const loansReducer = createReducer(
  initialState,
  on(setLoans, (state, action) => {
    return { ...state, loans: action.loans }
  })
);
