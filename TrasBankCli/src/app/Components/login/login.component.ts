import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription, map } from 'rxjs';
import { selectError, selectLoading } from '../../state/selectors/user.selectors';
import Swal from 'sweetalert2';
import { alertLoading, errorAlert } from '../Utils';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  loading$: Subscription;
  error$: Subscription;
  formSelect: boolean = true;
  
  constructor(private store: Store<any>) {
    this.loading$ = this.store.select(selectLoading).pipe(map(res => { return res })).subscribe((val) => { this.loading(val) });
    this.error$ = this.store.select(selectError).pipe(map(res => { return res })).subscribe((val) => { if (val != "") errorAlert(val) })
  }
  ngOnDestroy(): void {
    this.loading$.unsubscribe();
    this.error$.unsubscribe();
  }

  ngOnInit(): void {
  }

  changeLogin() {
    this.formSelect = false;
  }

  loading(value: boolean) {
    if (value) {
      alertLoading.fire();
    } else {
      alertLoading.close();
    }
  }
}
