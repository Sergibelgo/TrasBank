import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent {
  @Input() left: boolean = false;
  @Input() title: string = "";
  @Input() text: string = "";
  @Input() list: string[] = [];
  @Input() img: string = "assets/img/about/about-2.png";
  @Input() url: string | undefined;
}
