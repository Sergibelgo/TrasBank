import { Injectable } from '@angular/core';
import { url } from './base-service.service';
import { Scoring } from '../Models/scoring/scoring';

@Injectable({
  providedIn: 'root'
})
export class LoansService {
  baseUrl = `${url}Loans/`;
  scoringUrl = `${url}Scorings/`
  constructor() { }
  async loadLoansUser(jwt: string) {
    let result = await fetch(`${this.baseUrl}self`, {
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
  async checkScoring(jwt: string, scoring: Scoring) {
    let result = await fetch(`${this.scoringUrl}CheckScore`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      },
      body: JSON.stringify(scoring)
    })
    var data = await result.json();
    if (!result.ok) {
      if (data.error != undefined) {
        throw Error(data.error);
      } else if(data.errors != undefined) {
        let errData = Object.entries(data.errors);
        let err=errData.map(val => val.join(" : "));
        throw Error(err.join(","))
      }
      else {
        throw Error(data);
      }

    }
    return data;
  }
  async requestScoring(jwt: string, scoring: Scoring) {
    let result = await fetch(`${this.scoringUrl}ConfirmScore`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      },
      body: JSON.stringify(scoring)
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
