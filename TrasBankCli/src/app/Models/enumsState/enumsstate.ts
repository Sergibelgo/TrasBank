export interface Enumsstate {
  TransactionTypes: Enum[],
  WorkingStatuses: Enum[],
  AccountStatuses: Enum[],
  LoanTypes: LoanType[],
  WorkerStatuses: Enum[],
  AccountTypes:Enum[]
}
export interface Enum {
  Id: number,
  Name: string
}
export interface LoanType extends Enum {
  TIN: number,
  TAE: number,
  Percentaje: number
}
