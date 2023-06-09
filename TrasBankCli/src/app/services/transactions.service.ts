import { Injectable } from '@angular/core';
import { url } from './base-service.service';
import { TransactionDto } from '../Models/transactionDTO/transaction-dto';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {
  urlAccounts: string = `${url}Transactions/`;
  urlSelf: string = `${this.urlAccounts}self/`
  constructor() { }
  async getTransactions(jwt: string, id: string) {
    let url;
    if (id == "") {
      url = `${this.urlSelf}all`
    } else {
      url = `${this.urlSelf}${id}`
    }
    let result = await fetch(`${url}`, {
      method: "GET",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    });
    if (!result.ok) {
      return await result.text();

    }
    let dataResult = await result.json();
    
    return dataResult;
  }
  async getUserAccounts(jwt: string, otherUser: string) {
    let result = await fetch(`${url}Accounts/GetByUserName`, {
      method: "POST",
      body: JSON.stringify(otherUser),
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    });
    if (result.ok) {
      return await result.json();
    } else {
      throw Error("Not found")
    }
  }
  async makeTransaction(transaction: TransactionDto, jwt: string) {
    let result = await fetch(`${url}Transactions/Transfer`, {
      method: "POST",
      body: JSON.stringify(transaction),
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    });
    if (result.ok) {
      return;
    } else {
      let data = await result.text();
      throw new Error(data)
    }
  }
}
