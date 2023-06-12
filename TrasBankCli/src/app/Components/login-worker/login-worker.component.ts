import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { alertLoading, errorAlert } from '../Utils';
import { selectError, selectLoading } from '../../state/selectors/utils.selectors';
import { TranslateService } from '@ngx-translate/core';
import { selectJWT } from '../../state/selectors/user.selectors';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-worker',
  templateUrl: './login-worker.component.html',
  styleUrls: ['./login-worker.component.css']
})
export class LoginWorkerComponent implements OnInit,OnDestroy {

  loading$: Subscription = new Subscription();
  error$: Subscription = new Subscription();
  jwt$: Subscription = new Subscription();
  constructor(private store: Store<any>,private trans:TranslateService,private router:Router) {

  }
  ngOnDestroy(): void {
    this.loading$.unsubscribe();
    this.error$.unsubscribe();
    this.jwt$.unsubscribe();
  }
  ngOnInit(): void {
    this.error$ = this.store.select(selectError).subscribe((val) => { if (val != "") errorAlert(val) });
    this.loading$ = this.store.select(selectLoading).subscribe(val => { if (val) { alertLoading(this.trans.instant("Loading")) } })
    this.jwt$ = this.store.select(selectJWT).subscribe(val => { if (val != "") { this.router.navigate(["WorkerDash"]); } })
  }
}
