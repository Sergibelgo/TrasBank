import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { setIndex } from '../../../../state/actions/utils.actions';
import { selectNotReaded } from '../../../../state/selectors/messages.selectors';
import { Observable } from 'rxjs';
import { selectUserName } from '../../../../state/selectors/user.selectors';
declare var $: any;

@Component({
  selector: 'app-menu-sW',
  templateUrl: './menu-s.component.html',
  styleUrls: ['./menu-s.component.css']
})
export class MenuSWComponent {
  @Input() checkM: boolean=true;
  readed$: Observable<boolean>;
  username$: Observable<string | undefined>;
  constructor(private store: Store<any>) {
    this.readed$ = this.store.select(selectNotReaded);
    this.username$ = this.store.select(selectUserName);
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
