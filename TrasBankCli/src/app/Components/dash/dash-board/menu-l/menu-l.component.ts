import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { setIndex } from '../../../../state/actions/utils.actions';
import { Observable, Subscription } from 'rxjs';
import { selectNotReaded } from '../../../../state/selectors/messages.selectors';


@Component({
  selector: 'app-menu-l',
  templateUrl: './menu-l.component.html',
  styleUrls: ['./menu-l.component.css']
})
export class MenuLComponent {
  readed$: Observable<boolean>
  constructor(private store: Store<any>) {
    this.readed$ = this.store.select(selectNotReaded)
  }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
