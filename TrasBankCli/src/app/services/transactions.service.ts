import { Injectable } from '@angular/core';
import { url } from './base-service.service';

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
    let dataResult = await result.json();
    if (!result.ok) {
      if (dataResult.error != undefined) {
        throw Error(dataResult.error);
      } else {
        throw Error(dataResult);
      }

    }
    return dataResult;
  }
}
