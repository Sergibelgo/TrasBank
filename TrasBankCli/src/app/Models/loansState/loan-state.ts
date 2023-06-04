import { Loan } from "../loan/loan";

export interface LoanState {
  loans: Loan[];
  activeLoan: Loan;
}
