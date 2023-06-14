import { Loan } from "../loan/loan";
import { Scoring } from "../scoring/scoring";

export interface LoanState {
  loans: Loan[];
  activeLoan: Loan;
  scoringForm: Scoring;
}
