import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from '../../../../Models/User/user';
import { Store } from '@ngrx/store';
import { selectJWT, selectUser } from '../../../../state/selectors/user.selectors';
import Swal from 'sweetalert2';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../../../services/auth-service.service';
import { updateUserInfo } from '../../../../state/actions/auth.actions';
import { errorAlert, successAlert } from '../../../Utils';
declare var $: any;

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit,OnDestroy {
  user$: Subscription=new Subscription();
  user: User = { Age: new Date(), Email: "", FirstName: "", Income: 0, LastName: "", UserName: "", Address: "" ,Id:""}
  userUpdate: User = this.user;
  check: boolean = true;
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  constructor(private store: Store<any>,private trans:TranslateService,private service:AuthService) {
    
  }
  ngOnDestroy(): void {
    this.jwt$.unsubscribe();
    this.user$.unsubscribe();
    }
    ngOnInit(): void {
      this.user$ = this.store.select(selectUser).subscribe(val => { this.user = { ...val as User }; this.userUpdate = { ...val as User } });
      this.jwt$ = this.store.select(selectJWT).subscribe(val=>this.jwt=val)
    }
  submit() {
    Swal.fire({

      showCancelButton: true,
      icon: "info",
      text:this.trans.instant("Are you sure?")
    }).then(val => {
      if (val.isConfirmed) {
        this.service.updateUser(this.jwt, this.userUpdate).then(val => {
          this.store.dispatch(updateUserInfo({ user: this.userUpdate }));
          this.reset();
          successAlert(this.trans.instant("User information updated"))
        }).catch(val => {
          errorAlert(val)
        })
      }
    })
  }
  reset() {
    if (!this.check) {
      this.userUpdate = { ...this.user} 
      $("input").attr("readonly", true);
    } else {
      $("input[readonly]").attr("readonly", false);
    }
    this.check = !this.check;

  }
}
