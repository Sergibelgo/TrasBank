import { Injectable } from '@angular/core';
import { url } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class WorkersService {
  baseUrl: string = `${url}Workers/`;
  constructor() {
  }
  async getPublicInfo() {
    let result = await fetch(`${this.baseUrl}GetWorkersMail`);
    var data = await result.json();
    if (!result.ok) {
      throw Error(data);
    }
    return data;
  }
  async getCustomers(jwt: string) {
    let result = await fetch(`${url}Customers/customerWorkersSelf`, {
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
  async getPending(jwt: string) {
    let result = await fetch(`${url}Loans/LoansWaitingSelf`, {
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

}
