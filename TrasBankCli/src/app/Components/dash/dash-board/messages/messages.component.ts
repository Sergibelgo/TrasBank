import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject, Subscription } from 'rxjs';
import { Message } from '../../../../Models/message/message';
import { selectMessages } from '../../../../state/selectors/messages.selectors';
import { selectJWT } from '../../../../state/selectors/user.selectors';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { setActiveMessage, setMessages, setNotReaded, setReaded, updateReaded } from '../../../../state/actions/messages.actions';
declare var $: any;

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements AfterViewInit, OnInit {
  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | undefined;

  dtOptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject();

  messages$: Subscription;
  messages: Message[] = [];
  jwt$: Subscription;
  jwt = "";

  constructor(private store: Store<any>, private trans: TranslateService) {
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val);
    this.messages$ = this.store.select(selectMessages).pipe(val => val).subscribe((val) => { this.messages = val; this.updateDataTable() });
  }
  ngOnInit(): void {
    this.dtOptions = {
      columnDefs: [
        {
          targets: [0,5],
          visible: false,
          searchable: false
        }
      ],
      order: [[4, "asc"], [2, 'desc'], [1, "asc"]],
      responsive: true,
      lengthChange: false,
      pageLength: 10,
      language: {
        url: this.trans.currentLang == "es" ? `/assets/i18n/datatables.es.json` : `/assets/i18n/datatables.en.json`
      },
      rowCallback: (row: Node, data: any[] | Object, index: number) => {
        const self = this;
        $('td', row).off('click');
        $('td', row).on('click', () => {
          self.clickHandler(data);
        });
        return row;
      }
    }
    this.trans.onLangChange.subscribe((event: LangChangeEvent) => {
      this.updateDataTable();
    })
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next(undefined);
  }
  clickHandler(info: any) {
    this.store.dispatch(setActiveMessage({ Id: info[0] }));
    $(".modal").modal("show");
    if (info[5] == "false") {
      this.store.dispatch(setReaded({ jwt: this.jwt, Id: info[0] }));
      setTimeout(() => { this.store.dispatch(setNotReaded()) }, 5000)
    }
  }
  updateDataTable() {
    this.dtElement?.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Change Language
      this.dtOptions.language = { url: this.trans.currentLang == "es" ? `/assets/i18n/datatables.es.json` : `/assets/i18n/datatables.en.json` }
      // Call the dtTrigger to rerender again
      this.dtTrigger.next(undefined);
    });
  }
}
