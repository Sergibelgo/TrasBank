import { Component } from '@angular/core';
/**
 * This component displays the "To top" button on the screen
 * Use it only once per route container since multiple will get stack on top of each other
 * Right now is used on app module so it is not necesary to use it again
 * Usage example:
 * <app-scroll-top></app-scroll-top>
 * <app-other></app-other>
 * <app-another></app-another>
 * ...
 */
@Component({
  selector: 'app-scroll-top',
  templateUrl: './scroll-top.component.html',
  styleUrls: ['./scroll-top.component.css']
})
export class ScrollTopComponent {
  toTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }
}
