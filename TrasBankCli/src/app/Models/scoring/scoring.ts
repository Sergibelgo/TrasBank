export interface Scoring {
  Name: string,
  Ammount: number,
  TotalInstallments: number,
  LoanType: number,
  TIN_TAE: number,
  Expenses: { Description: string, Spend: number }[],
  Deposit:number
}
