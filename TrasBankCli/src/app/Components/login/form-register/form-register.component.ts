import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { setLoad, tryLogIn } from '../../../state/actions/auth.actions';

@Component({
  selector: 'app-form-register',
  templateUrl: './form-register.component.html',
  styleUrls: ['./form-register.component.css']
})
export class FormRegisterComponent {
  frRegister: FormGroup;
  constructor(private frmBuilder: FormBuilder, private store: Store<any>) {
    this.frRegister = this.frmBuilder.group({
      username: new FormControl("", [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
      ])
    });
  }
  ngOnInit(): void {

  }
  register() {
    if (this.frRegister.valid) {
      this.store.dispatch(setLoad({ load: true }));
      const data = this.frRegister.value;
      this.store.dispatch(tryLogIn(data));
    }
  }

}
