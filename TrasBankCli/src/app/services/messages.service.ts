import { Injectable } from '@angular/core';
import { url } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {
  urlBase: string = `${url}Messages/`
  constructor() { }
  async getMessages(jwt: string) {
    let result = await fetch(`${this.urlBase}self`, {
      method: "GET",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    });
    if (result.ok) {
      return await result.json();
    } else {
      let data = await result.text();
      throw Error(data);
    }
  }
  async setReaded(jwt: string, Id: string) {
    let result = await fetch(`${this.urlBase}Read`, {
      method: "POST",
      body: JSON.stringify(Id),
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      }
    });
    if (result.ok) {
      return Id;
    } else {
      let data = await result.text();
      throw Error(data);
    }
  }
}
