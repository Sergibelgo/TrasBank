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
  username$: Subscription = new Subscription()
  username: string | undefined;
  @Input() checkM: boolean = true;
  constructor(private store: Store<any>) {
    this.readed$ = this.store.select(selectNotReaded);
  }
  ngOnDestroy(): void {
    this.username$.unsubscribe()
    }
  ngOnInit(): void {
    this.username$ = this.store.select(selectUserName).subscribe(val=>this.username=val);
    }
  changeIndex(index: number) {
    this.store.dispatch(setIndex({ index: index }));
  }
}
