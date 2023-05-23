import { Account } from "../Account/account";

export class User {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  income: number;
  accounts: Account[];
  age: Date;
  constructor(name: string, lastName: string, username: string, email: string, income: number, accounts: Account[], age: Date) {
    this.firstName = name;
    this.lastName = lastName;
    this.username = username;
    this.email = email;
    this.income = income;
    this.accounts = accounts;
    this.age = age;
  }
}
