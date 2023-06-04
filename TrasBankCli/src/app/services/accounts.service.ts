import { Injectable } from '@angular/core';
import { url } from './base-service.service';
import { AccountDTO } from '../Models/createAccount/account-dto';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  urlAccounts: string = `${url}Accounts/`;
  constructor() { }
  async getAccounts(jwt: string) {
    let result = await fetch(`${this.urlAccounts}self`, {
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
  async createAccount(jwt: string, account: AccountDTO) {
    let result = await fetch(this.urlAccounts, {
      method: "POST",
      body: JSON.stringify(account),
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
  async updateAccountName(name: string, id: string,jwt:string) {
    let result = await fetch(`${this.urlAccounts}${id}`, {
      method: "PUT",
      body: JSON.stringify(name),
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    });
    
    if (!result.ok) {
      let dataResult = await result.json();
      if (dataResult.error != undefined) {
        throw Error(dataResult.error);
      } else {
        throw Error(dataResult);
      }
    }
  }
}
