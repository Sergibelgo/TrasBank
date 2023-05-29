import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { setIndex } from '../../../../state/actions/auth.actions';

@Component({
  selector: 'app-menu-l',
  templateUrl: './menu-l.component.html',
  styleUrls: ['./menu-l.component.css']
})
export class MenuLComponent {
  constructor(private store: Store<any>) {

  }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
