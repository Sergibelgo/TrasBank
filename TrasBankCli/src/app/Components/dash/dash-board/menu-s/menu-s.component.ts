import { Component } from '@angular/core';
declare var $: any;

@Component({
  selector: 'app-menu-s',
  templateUrl: './menu-s.component.html',
  styleUrls: ['./menu-s.component.css']
})
export class MenuSComponent {
  showMenu() {
    $("#container").removeClass("d-none").addClass("d-block")
  }
  closeMenu() {
    $("#container").removeClass("d-block").addClass("d-none")
  }
}
