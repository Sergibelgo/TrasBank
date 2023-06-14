import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Message } from '../../../../../Models/message/message';
import { Subscription } from 'rxjs';
import { Store } from '@ngrx/store';
import { selectActiveMessage } from '../../../../../state/selectors/messages.selectors';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit, OnDestroy {
  message$!: Subscription;
  message!: Message ;
;
  constructor(private store:Store<any>) {
    
  }
  ngOnDestroy(): void {
     
  }
  ngOnInit(): void {
    this.message$ = this.store.select(selectActiveMessage).subscribe(val => this.message = val);
  }
  
}
