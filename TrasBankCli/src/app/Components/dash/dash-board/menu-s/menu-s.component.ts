import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { setIndex } from '../../../../state/actions/utils.actions';
import { selectNotReaded } from '../../../../state/selectors/messages.selectors';
import { Observable } from 'rxjs';
declare var $: any;

@Component({
  selector: 'app-menu-s',
  templateUrl: './menu-s.component.html',
  styleUrls: ['./menu-s.component.css']
})
export class MenuSComponent {

  readed$: Observable<boolean>
  constructor(private store: Store<any>) {
    this.readed$ = this.store.select(selectNotReaded)
  }
  showMenu() {
    $("#container").removeClass("d-none").addClass("d-block")
  }
  closeMenu() {
    $("#container").removeClass("d-block").addClass("d-none")
  }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
