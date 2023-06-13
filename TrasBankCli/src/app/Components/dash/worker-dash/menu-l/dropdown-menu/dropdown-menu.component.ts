import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { setIndex } from '../../../../../state/actions/utils.actions';

@Component({
  selector: 'app-dropdown-menuW',
  templateUrl: './dropdown-menu.component.html',
  styleUrls: ['./dropdown-menu.component.css']
})
export class DropdownMenuWComponent {
  @Input() dropType: string = "dropdown"
  @Input() checkM: boolean = true;
  constructor(private store: Store<any>) {

  }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
