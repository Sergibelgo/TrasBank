import { Component, Input } from '@angular/core';
import { Message } from '../../../../../Models/message/message';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent {
  @Input() message!: Message
}
