import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { setIndex } from '../../../../../state/actions/utils.actions';

@Component({
  selector: 'app-dropdown-menu',
  templateUrl: './dropdown-menu.component.html',
  styleUrls: ['./dropdown-menu.component.css']
})
export class DropdownMenuComponent {
  @Input() dropType: string = "dropdown"
  @Input() username: string | null | undefined = null;
  constructor(private store: Store<any>) {

  }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
