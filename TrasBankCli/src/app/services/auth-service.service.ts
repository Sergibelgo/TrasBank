import { Injectable } from '@angular/core';
import { url } from './base-service.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor() {
  }
  async login(password: string, username?: string, email?: string) {
    let usernam = username ?? email;
    let result = await fetch(`${url}AspNetUsers/Login`, {
      body: JSON.stringify({ username, email, password }),
      method: "POST",
      headers: {
        "Content-type": "application/json"
      }
    });
    var data = await result.json();
    if (!result.ok) {
      throw Error(data);
    }
    return data;
  }
}
