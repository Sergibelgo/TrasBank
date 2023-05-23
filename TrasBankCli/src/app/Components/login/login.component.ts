import { Component,  OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import {  map } from 'rxjs';
import { selectError, selectLoading } from '../../state/selectors/user.selectors';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loading$ = this.store.select(selectLoading);
  error$ = this.store.select(selectError);
  formSelect: boolean = true;
  alertLoading: any = Swal.mixin({
    title: "Loading...",
    icon: 'info',
    showConfirmButton: false,
    allowOutsideClick: false
  });
  constructor(private store: Store<any>) {
    this.loading$.pipe(map(res => { return res })).subscribe((val) => { this.loading(val) });
    this.error$.pipe(map(res => { return res })).subscribe((val) => { if (val!="") this.error(val) })
  }

  ngOnInit(): void {
  }

  changeLogin() {
    this.formSelect = false;
  }

  loading(value: boolean) {
    if (value) {
      this.alertLoading.fire();
    } else {
      this.alertLoading.close();
    }
  }
  error(val: string) {
    Swal.fire({
      icon: "error",
      title:val
    })
  }
}
