import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { selectIndex, selectSuccess } from '../../../state/selectors/utils.selectors';
import { successAlert } from '../../Utils';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { selectJWT } from '../../../state/selectors/user.selectors';
import { loadUser } from '../../../state/actions/auth.actions';
import { loadMessages } from '../../../state/actions/messages.actions';
import { loadPendingLoans } from '../../../state/actions/worker.actions';

@Component({
  selector: 'app-worker-dash',
  templateUrl: './worker-dash.component.html',
  styleUrls: ['./worker-dash.component.css']
})
export class WorkerDashComponent implements OnInit, OnDestroy {
  index$: Subscription = new Subscription();
  index: number = 0;
  success$: Subscription = new Subscription();
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  constructor(private store: Store<any>,private trans:TranslateService,private router:Router) {

  }
  ngOnDestroy(): void {
    this.index$.unsubscribe();
    this.jwt$.unsubscribe();
  }
  ngOnInit(): void {
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt=val)
    if (this.jwt == "") {
      this.router.navigate(["loginWorker"]);
    } else {
      this.index$ = this.store.select(selectIndex).subscribe(val => this.index = val)
      this.success$ = this.store.select(selectSuccess).pipe(val => val).subscribe(val => { if (val != "") { successAlert(this.trans.instant("Loged in successfully")) } })
      this.store.dispatch(loadMessages({ jwt: this.jwt }));
      this.store.dispatch(loadPendingLoans({ jwt: this.jwt }));

    }
    
  }
}
