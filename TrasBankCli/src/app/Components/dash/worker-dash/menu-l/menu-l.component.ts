import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { setIndex } from '../../../../state/actions/utils.actions';
import { Observable, Subscription } from 'rxjs';
import { selectNotReaded } from '../../../../state/selectors/messages.selectors';
import { selectUserName } from '../../../../state/selectors/user.selectors';


@Component({
  selector: 'app-menu-lW',
  templateUrl: './menu-l.component.html',
  styleUrls: ['./menu-l.component.css']
})
export class MenuLWComponent implements OnInit, OnDestroy {
  readed$: Observable<boolean>;
  @Input() checkM: boolean = true;
  constructor(private store: Store<any>) {
    this.readed$ = this.store.select(selectNotReaded);
  }
  ngOnDestroy(): void {
    }
  ngOnInit(): void {
    }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
