export interface Scoring {
  Name: string,
  Ammount: number,
  TotalInstallments: number,
  LoanTypeId: number,
  TIN_TAE: number,
  Expenses: { Description: string, Spend: number }[],
  Deposit:number
}
