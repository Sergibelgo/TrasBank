export interface Loan {
  Id: string;
  CustomerName: string;
  CustomerId: string;
  Ammount: number;
  InterestRate: number;
  StartDate: Date;
  EndDate: Date;
  TotalInstallments: number;
  RemainingInstallments: number;
  RemainingAmmount: number;
  LoanStatus: string;
  LoanType: string;
}
