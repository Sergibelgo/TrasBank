import { Component } from '@angular/core';
import { setIndex } from '../../../../state/actions/auth.actions';
import { Store } from '@ngrx/store';
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
  constructor(private store: Store<any>) {

  }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
