import { Injectable } from '@angular/core';
import { url } from './base-service.service';

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
}
