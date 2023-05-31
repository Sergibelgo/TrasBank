export interface Enumsstate {
  TransactionTypes: Enum[],
  WorkingStatuses: Enum[],
  AccountStatuses: Enum[],
  LoanTypes: Enum[],
  WorkerStatuses: Enum[],
  AccountTypes:Enum[]
}
export interface Enum {
  Id: number,
  Name: string
}
