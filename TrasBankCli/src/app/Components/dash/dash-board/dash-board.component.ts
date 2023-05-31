import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription, first } from 'rxjs';
import { User } from '../../../Models/User/user';
import { selectJWT, selectUser } from '../../../state/selectors/user.selectors';
import { loadUser, setUserJWT } from '../../../state/actions/auth.actions';
import { Router } from '@angular/router';
import { alertLoading, successAlert } from '../../Utils';
import { selectIndex, selectLoading, selectSuccess } from '../../../state/selectors/utils.selectors';
import { TranslateService } from '@ngx-translate/core';
declare var $: any;

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.css']
})
export class DashBoardComponent implements OnInit, OnDestroy {
  user$: Subscription;
  user: User | null = null;
  jwt$: Subscription;
  jwt: string | null = null;
  index$: Subscription;
  index: number = 0;
  loading$: Subscription;
  success$: Subscription;
  constructor(private store: Store<any>, private router: Router, private transService: TranslateService) {
    this.user$ = this.store.select(selectUser).pipe((data) => data).subscribe((val) => this.user = val);
    this.jwt$ = this.store.select(selectJWT).pipe(data => data).subscribe(val => this.jwt = val);
    this.index$ = this.store.select(selectIndex).pipe(data => data).subscribe(val => this.index = val);
    this.loading$ = this.store.select(selectLoading).pipe(val => val).subscribe(val => this.loading(val));
    this.success$ = this.store.select(selectSuccess).pipe(val => val).subscribe(val => this.success(val));
  }
  ngOnDestroy(): void {
    this.user$.unsubscribe();
    this.jwt$.unsubscribe();
    this.index$.unsubscribe();
    this.loading$.unsubscribe();
  }
  ngOnInit(): void {
    if (this.jwt == "") {
      if (localStorage.getItem("userTokenIdentification") == undefined) {
        this.router.navigate(["login"]);
      } else {
        this.store.dispatch(setUserJWT({ userJWT: localStorage.getItem("userTokenIdentification") as string }))
      }
    }
    this.store.dispatch(loadUser({ jwt: this.jwt as string }));
  }
  loading(value: boolean) {
    if (value) {
      alertLoading(this.transService.instant("Loading"));
    }
  }
  success(val: string) {
    if (val != "") {
      successAlert(val);
      $(".modal").modal("hide");
    }
  }

}
