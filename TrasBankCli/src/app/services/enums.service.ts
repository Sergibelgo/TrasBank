import { Injectable } from '@angular/core';
import { url } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class EnumsService {
  baseUrl: string = `${url}Enums/`;
  constructor() {
  }
  async getTransactionTypes() {
    let result = await fetch(`${this.baseUrl}/TransactionTypes`);
    var data = await result.json();
    if (!result.ok) {
      throw Error(data);
    }
    console.log(data);
    return data;
  }
}
