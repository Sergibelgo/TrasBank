import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { User } from '../../../../Models/User/user';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { loadCustomers } from '../../../../state/actions/worker.actions';
import { selectJWT } from '../../../../state/selectors/user.selectors';
import { selectCustomers } from '../../../../state/selectors/worker.selectors';
import { setIndex } from '../../../../state/actions/utils.actions';

@Component({
  selector: 'app-clients-info',
  templateUrl: './clients-info.component.html',
  styleUrls: ['./clients-info.component.css']
})
export class ClientsInfoComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | undefined;

  dtOptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject();
  users$: Subscription = new Subscription();
  users: User[] = [];
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  constructor(private trans: TranslateService, private store: Store<any>) {

  }
  ngOnDestroy(): void {
    this.users$.unsubscribe()
  }
  ngOnInit(): void {
    this.dtOptions = {
      order: [[2, "asc"], [1, 'asc']],
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
    this.jwt$ = this.store.select(selectJWT).subscribe(val => this.jwt = val)
    if (this.jwt != "") {
      this.users$ = this.store.select(selectCustomers).subscribe(val => { this.users = val; this.updateDataTable() })
      if (this.users.length == 0) {
        this.store.dispatch(loadCustomers({ jwt: this.jwt }))
      }
    }
    
  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(undefined);

  }
  clickHandler(data: any) {

    this.store.dispatch(setIndex({index:1}))
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
