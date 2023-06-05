import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from '../../../../Models/User/user';
import { Store } from '@ngrx/store';
import { selectUser } from '../../../../state/selectors/user.selectors';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  user$: Subscription;
  user: User = { Age: new Date(), Email: "", FirstName: "", Income: 0, LastName: "", UserName: "",Address:"" }
  constructor(private store: Store<any>) {
    this.user$ = this.store.select(selectUser).subscribe(val => this.user = { ...val as User, Age:new Date((val  as User).Age) })
  }
}
