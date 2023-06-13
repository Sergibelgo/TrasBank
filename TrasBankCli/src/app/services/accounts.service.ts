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
    let dataResult = result.ok ? await result.json() : await result.text();
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
  async getByCustomerId(jwt: string,id:string) {
    let result = await fetch(`${this.urlAccounts}ByCustomerId/${id}`, {
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
  async updateAccountStatus(jwt: string, id: string, status: number) {
    let result = await fetch(`${this.urlAccounts}Status/${id}`, {
      method: "PUT",
      body: JSON.stringify(status),
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
    return true;
  }
}
