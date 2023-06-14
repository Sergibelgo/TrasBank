import { Injectable } from '@angular/core';
import { url } from './base-service.service';
import { UserRegister } from '../Models/UserRegister/user-register';
import { User } from '../Models/User/user';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  urlUsers: string = `${url}AspNetUsers/`;
  urlCustomers: string = `${url}Customers/`;
  constructor() {
  }
  async login(password: string, usernam?: string, email?: string) {
    let username = usernam ?? email;
    let result = await fetch(`${this.urlUsers}Login`, {
      body: JSON.stringify({ username, email, password }),
      method: "POST",
      headers: {
        "Content-type": "application/json"
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
  async register(data: UserRegister) {
    let result = await fetch(`${this.urlCustomers}`, {
      body: JSON.stringify(data),
      method: "POST",
      headers: {
        "Content-type": "application/json"
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
  async loadUser(jwt: string) {
    let result = await fetch(`${this.urlCustomers}self`, {
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
  async updateUser(jwt: string, user: User) {
    let result = await fetch(`${this.urlCustomers}self`, {
      method: "PUT",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      },
      body: JSON.stringify(user)
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
  }
}
