import { Component,  OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { tryLogIn } from '../../../state/actions/auth.actions';
import { setLoad } from '../../../state/actions/utils.actions';

@Component({
  selector: 'app-form-login',
  templateUrl: './form-login.component.html',
  styleUrls: ['./form-login.component.css']
})
export class FormLoginComponent implements OnInit {
  frlogin: FormGroup;
  constructor(private frmBuilder: FormBuilder, private store: Store<any>) {
    this.frlogin = this.frmBuilder.group({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [
        Validators.required,
      ]),
    });
  }
  ngOnInit(): void {

  }
  login() {
    if (this.frlogin.valid) {
      this.store.dispatch(setLoad({ load: true }));
      const data = this.frlogin.value;
      this.store.dispatch(tryLogIn(data));
    }
  }
}
