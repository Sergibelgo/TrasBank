import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription, map } from 'rxjs';
import { selectError, selectJWT, selectLoading } from '../../state/selectors/user.selectors';
import Swal from 'sweetalert2';
import { alertLoading, errorAlert } from '../Utils';
import { Router } from '@angular/router';
import { setUserJWT } from '../../state/actions/auth.actions';
declare var $: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  loading$: Subscription;
  error$: Subscription;
  userJWT$: Subscription;
  formSelect: boolean = true;
  jwt: string | null = localStorage.getItem("userTokenIdentification");

  constructor(private store: Store<any>, private router: Router) {
    this.loading$ = this.store.select(selectLoading).pipe(map(res => { return res })).subscribe((val) => { this.loading(val) });
    this.error$ = this.store.select(selectError).pipe(map(res => { return res })).subscribe((val) => { if (val != "") errorAlert(val) });
    this.userJWT$ = this.store.select(selectJWT).pipe(map(res => res)).subscribe((val)=>this.logedIn(val))
  }
  ngOnDestroy(): void {
    this.loading$.unsubscribe();
    this.error$.unsubscribe();
  }

  ngOnInit(): void {
    if (this.jwt != null) {
      this.store.dispatch(setUserJWT({ userJWT: this.jwt }));
      this.router.navigate(["dash"]);
    }
  }

  changeForm( check: boolean) {
    let button = $(".card-footer button:not(:disabled)").get();
    $("button[disabled]").prop('disabled', (i: any, v: any) => !v);
    $(button).prop('disabled', (i: any, v: any) => !v);
    this.formSelect = check;
  }

  loading(value: boolean) {
    if (value) {
      alertLoading.fire();
    } else {
      alertLoading.close();
    }
  }
  logedIn(val: any) {
    if (val.accessToken != null && val.accessToken != "") {
      if (this.jwt == null || this.jwt != val.accessToken) {
        localStorage.setItem("userTokenIdentification", val.accessToken);
      }
      this.router.navigate(["dash"]);
    }
  }
}
