import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { selectJWT } from '../../../../../state/selectors/user.selectors';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { setError, setLoad, setSuccess } from '../../../../../state/actions/utils.actions';
import { MessagesService } from '../../../../../services/messages.service';
import { TranslateService } from '@ngx-translate/core';
import { errorAlert, successAlert } from '../../../../Utils';
declare var $: any;

@Component({
  selector: 'app-create-message',
  templateUrl: './create-message.component.html',
  styleUrls: ['./create-message.component.css']
})
export class CreateMessageComponent {
  jwt$: Subscription;
  jwt: string = "";
  frmCreateMessage: FormGroup;
  constructor(private store: Store<any>, private frmBuilder: FormBuilder, private msgService: MessagesService, private trans: TranslateService) {
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val);
    this.frmCreateMessage = this.frmBuilder.group({
      reciverUserName: new FormControl("", [Validators.required]),
      title: new FormControl("", [Validators.required]),
      body: new FormControl("", [Validators.required])
    })
  }
  sendMessage() {
    if (this.frmCreateMessage.valid) {
      this.store.dispatch(setLoad({ load: true }));
      this.msgService.sendMessage(this.jwt, this.frmCreateMessage.value).then((msg) => {
        successAlert(this.trans.instant("Message sended succesfully"));
        $("#ModalCreateMessage").modal("hide");
      }).catch((err: Error) => {
        errorAlert(err.message)
      }).finally(() => {
        this.store.dispatch(setLoad({load:false}))
      })

    }
  }

}
