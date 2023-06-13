import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { tryRegister } from '../../../state/actions/auth.actions';
import { loadWorkingStatuses } from '../../../state/actions/enums.actions';
import { selectWorkingStatuses } from '../../../state/selectors/enums.selectors';
import { Observable, Subscription, take } from 'rxjs';
import { Enum } from '../../../Models/enumsState/enumsstate';
import { selectWorkersPublicInfo } from '../../../state/selectors/worker.selectors';
import { WorkerPublicInfo } from '../../../Models/Worker/worker';
import { loadPublicInfo } from '../../../state/actions/worker.actions';
import { UserRegister } from '../../../Models/UserRegister/user-register';
import { setLoad } from '../../../state/actions/utils.actions';

@Component({
  selector: 'app-form-register',
  templateUrl: './form-register.component.html',
  styleUrls: ['./form-register.component.css']
})
export class FormRegisterComponent implements OnInit, OnDestroy {
  frRegister: FormGroup;
  workingStatuses$: Subscription;
  workingStatuses?: Enum[];
  workers$: Subscription;
  workers?: WorkerPublicInfo[];

  constructor(private frmBuilder: FormBuilder, private store: Store<any>) {
    this.frRegister = this.frmBuilder.group({
      username: new FormControl("", [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
      ]),
      firstname: new FormControl("", [Validators.required]),
      lastname: new FormControl("", [Validators.required]),
      address: new FormControl("", [Validators.required]),
      age: new FormControl("", [Validators.required]),
      income: new FormControl("", [Validators.required]),
      workStatusId: new FormControl("", [Validators.required]),
      workerEmail: new FormControl("", [Validators.required])
    });
    this.workingStatuses$ = this.store.select(selectWorkingStatuses).subscribe((val) => {
      this.workingStatuses = val;
    });
    this.workers$ = this.store.select(selectWorkersPublicInfo).subscribe((val) => this.workers = val);
  }
  ngOnDestroy(): void {
    this.workingStatuses$.unsubscribe();
    this.workers$.unsubscribe();
  }
  ngOnInit(): void {
      this.store.dispatch(loadWorkingStatuses());
    if (this.workers==null ||this.workers?.length == 1) {
      this.store.dispatch(loadPublicInfo());
    }
  }
  register() {
    if (this.frRegister.valid) {
      this.store.dispatch(setLoad({ load: true }));
      const userData = this.frRegister.value;
      this.store.dispatch(tryRegister(userData));
    }
  }

}
