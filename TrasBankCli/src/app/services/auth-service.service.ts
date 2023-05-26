import { Injectable } from '@angular/core';
import { url } from './base-service.service';
import { UserRegister } from '../Models/UserRegister/user-register';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor() {
  }
  async login(password: string, usernam?: string, email?: string) {
    let username = usernam ?? email;
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
  async register(data: UserRegister) {
    let result = await fetch(`${url}Customers`, {
      body: JSON.stringify(data),
      method: "POST",
      headers: {
        "Content-type": "application/json"
      }
    });
    let dataResult = await result.json();
    if (!result.ok) {
      throw Error(dataResult.error);
    }
    return dataResult;
  }
}
