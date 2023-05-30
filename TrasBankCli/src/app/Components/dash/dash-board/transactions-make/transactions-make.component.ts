import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-transactions-make',
  templateUrl: './transactions-make.component.html',
  styleUrls: ['./transactions-make.component.css']
})
export class TransactionsMakeComponent {
  frmTransaction: FormGroup;
  constructor(private store: Store<any>, private frmBuilder: FormBuilder) {
    this.frmTransaction = this.frmBuilder.group({
      username: new FormControl('', [Validators.required]),
      account: new FormControl("", [Validators.required])
    })

  }
}
