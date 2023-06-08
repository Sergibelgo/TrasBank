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
    let result = await fetch(`${this.baseUrl}TransactionTypes`);
    var data = await result.json();
    if (!result.ok) {
      throw Error(data);
    }
    return data;
  }
  async getWorkingStatuses() {
    let result = await fetch(`${this.baseUrl}CustomerWorkingStatuses`);
    var data = await result.json();
    if (!result.ok) {
      throw Error(data);
    }
    return data;
  }
  async getAccountTypes() {
    let result = await fetch(`${this.baseUrl}AccountTypes`);
    var data = await result.json();
    if (!result.ok) {
      throw Error(data);
    }
    return data;
  }
  async getLoansTypes() {
    let result = await fetch(`${this.baseUrl}LoanTypes`);
    var data = await result.json();
    if (!result.ok) {
      throw Error(data);
    }
    return data;
  }
}
