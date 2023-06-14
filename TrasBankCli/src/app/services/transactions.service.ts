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
  async getByCustomerId(jwt: string, id: string) {
    let result = await fetch(`${this.urlAccounts}ByUserId/${id}`, {
      method: "GET",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    })
    var data = await result.json();
    if (!result.ok) {
      if (data.error != undefined) {
        throw Error(data.error);
      } else if (data.errors != undefined) {
        let errData = Object.entries(data.errors);
        let err = errData.map(val => val.join(" : "));
        throw Error(err.join(","))
      }
      else {
        throw Error(data);
      }

    }
    return data;
  }
  async AddorRemoveMoney(jwt: string, id: string, info: {quantity:number,concept:string}) {
    let result = await fetch(`${this.urlAccounts}AddorRemoveMoney/${id}`, {
      method: "POST",
      body:JSON.stringify(info),
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    })
    
    if (!result.ok) {
      var data = await result.json();
      if (data.error != undefined) {
        throw Error(data.error);
      } else if (data.errors != undefined) {
        let errData = Object.entries(data.errors);
        let err = errData.map(val => val.join(" : "));
        throw Error(err.join(","))
      }
      else {
        throw Error(data);
      }

    }
    return data;
  }
}
