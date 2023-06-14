import { Component, Input } from '@angular/core';
import { Transaction } from '../../../../../Models/Transaction/transaction';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-modal-transaction',
  templateUrl: './modal-transaction.component.html',
  styleUrls: ['./modal-transaction.component.css']
})
export class ModalTransactionComponent {
  @Input()
  transaction!: Transaction;
  lang: string;
  options: any = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
  constructor(private translateService: TranslateService) {
    this.lang = translateService.currentLang;
  }
}
