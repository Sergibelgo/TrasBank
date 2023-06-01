import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { Message } from '../../../../Models/message/message';
import { selectMessages } from '../../../../state/selectors/messages.selectors';
import { loadMessages } from '../../../../state/actions/messages.actions';
import { DataTablesModule } from "angular-datatables";
import { selectJWT } from '../../../../state/selectors/user.selectors';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent {
  messages$: Subscription;
  messages: Message[] = [];
  jwt$: Subscription;
  jwt = "";
  dtOptions: DataTables.Settings = {};
  constructor(private store: Store<any>) {
    this.dtOptions = {
      order: [[3, "asc"], [1, 'desc'], [0, "asc"]],
      responsive:true
    }
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val);
    this.messages$ = this.store.select(selectMessages).pipe(val => val).subscribe((val) => this.messages = val);
  }
}
