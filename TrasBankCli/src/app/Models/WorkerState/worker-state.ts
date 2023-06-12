import { User } from "../User/user";
import { WorkerPublicInfo } from "../Worker/worker";
import { Loan } from "../loan/loan";

export interface WorkerState {
  workersPublicInfo: WorkerPublicInfo[];
  customers: User[];
  loansPending: Loan[];
  activeUser: string;
}
