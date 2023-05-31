import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { resetUser } from '../../../../state/actions/auth.actions';
import { Router } from '@angular/router';
import { resetAccounts } from '../../../../state/actions/accounts.actions';
import { resetUtils } from '../../../../state/actions/utils.actions';

@Component({
  selector: 'app-logout-button',
  templateUrl: './logout-button.component.html',
  styleUrls: ['./logout-button.component.css']
})
export class LogoutButtonComponent {
  constructor(private store: Store<any>, private router: Router) {

  }
  logout() {
    this.store.dispatch(resetUser());
    this.store.dispatch(resetAccounts())
    this.store.dispatch(resetUtils())
    localStorage.removeItem("userTokenIdentification");
    this.router.navigate(["login"]);
  }
}
