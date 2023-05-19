import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-us',
  templateUrl: './us.component.html',
  styleUrls: ['./us.component.css']
})
export class UsComponent {
  @Input() title: string = "";
  @Input() text: string = "";
}
