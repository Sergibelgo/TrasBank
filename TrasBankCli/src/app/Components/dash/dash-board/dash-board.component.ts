import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription, first } from 'rxjs';
import { User } from '../../../Models/User/user';
import { selectJWT, selectUser } from '../../../state/selectors/user.selectors';
import { loadUser, setUserJWT } from '../../../state/actions/auth.actions';
import { Router } from '@angular/router';

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
  constructor(private store: Store<any>, private router: Router) {
    this.user$ = this.store.select(selectUser).pipe((data) => data).subscribe((val) => this.user = val);
    this.jwt$ = this.store.select(selectJWT).pipe(data => data).subscribe(val => this.jwt = val);
  }
  ngOnDestroy(): void {
    this.user$.unsubscribe();
  }
  ngOnInit(): void {
    if (this.jwt == "") {
      if (localStorage.getItem("userTokenIdentification") == undefined) {
        this.router.navigate([""]);
      } else {
        this.store.dispatch(setUserJWT({ userJWT: localStorage.getItem("userTokenIdentification") as string }))
      }
    }
    this.store.dispatch(loadUser({ jwt: this.jwt as string }));
  }

}
