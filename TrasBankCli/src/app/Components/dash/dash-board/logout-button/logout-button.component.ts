import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { resetUser } from '../../../../state/actions/auth.actions';
import { Router } from '@angular/router';

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
    localStorage.removeItem("userTokenIdentification");
    this.router.navigate([""]);
  }
}
