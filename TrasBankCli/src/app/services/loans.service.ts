import { Injectable } from '@angular/core';
import { url } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class LoansService {
  baseUrl = `${url}Loans/`;
  constructor() { }
  async loadLoansUser(jwt: string) {
    let result =await fetch(`${this.baseUrl}self`, {
      method: "GET",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    });
    var data = await result.json();
    if (!result.ok) {
      if (data.error != undefined) {
        throw Error(data.error);
      } else {
        throw Error(data);
      }

    }
    return data;
  }
}
