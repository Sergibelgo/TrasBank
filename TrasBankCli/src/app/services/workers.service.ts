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
    console.log(data);
    return data;
  }
}
