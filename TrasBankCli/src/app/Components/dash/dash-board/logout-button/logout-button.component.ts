/**
 * Component that show a link to disconnect from account and reset state.
 * If check is true it sends to the login of user
 * else it sends to the login of worker
 */
import { Component, Input } from '@angular/core';
import { Store } from '@ngrx/store';
import { resetUser } from '../../../../state/actions/auth.actions';
import { Router } from '@angular/router';
import { resetAccounts } from '../../../../state/actions/accounts.actions';
import { resetUtils } from '../../../../state/actions/utils.actions';
import { resetMessages } from '../../../../state/actions/messages.actions';

@Component({
  selector: 'app-logout-button',
  templateUrl: './logout-button.component.html',
  styleUrls: ['./logout-button.component.css']
})
export class LogoutButtonComponent {
  @Input() check: boolean = true;
  constructor(private store: Store<any>, private router: Router) {

  }
  logout() {
    this.store.dispatch(resetUser());
    this.store.dispatch(resetAccounts())
    this.store.dispatch(resetUtils())
    this.store.dispatch(resetMessages())
    localStorage.removeItem("userTokenIdentification");
    if (this.check) {
      this.router.navigate(["login"]);
    } else {
      this.router.navigate(["loginWorker"]);
    }
   
  }
}
