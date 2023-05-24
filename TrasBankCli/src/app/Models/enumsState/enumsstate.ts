export interface Enumsstate {
  TransactionTypes: Enum[],
  WorkingStatuses: Enum[],
  AccountStatuses: Enum[],
  LoanTypes: Enum[],
  WorkerStatuses: Enum[]
}
export interface Enum {
  id: number,
  Name: string
}
