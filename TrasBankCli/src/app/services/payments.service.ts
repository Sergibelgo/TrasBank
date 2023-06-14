import { Injectable } from '@angular/core';
import { PaymentDTO } from '../Models/paymentDTO/payment-dto';
import { url } from './base-service.service';

@Injectable({
  providedIn: 'root'
})
export class PaymentsService {
  paymentsUrl = `${url}Payments/`;
  async makePayment(jwt: string, payment: PaymentDTO,checkW:string|null="") {
    let result = await fetch(`${this.paymentsUrl}MakePayment${checkW??""}`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
        "Authorization": `Bearer ${jwt}`
      },
      body: JSON.stringify(payment)
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
