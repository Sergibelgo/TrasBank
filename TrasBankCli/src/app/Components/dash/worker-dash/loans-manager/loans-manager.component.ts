import { Component, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { User } from '../../../../Models/User/user';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { selectJWT } from '../../../../state/selectors/user.selectors';
import { Loan } from '../../../../Models/loan/loan';
import { selectPendingLoans } from '../../../../state/selectors/worker.selectors';
import { loadPendingLoans } from '../../../../state/actions/worker.actions';

@Component({
  selector: 'app-loans-manager',
  templateUrl: './loans-manager.component.html',
  styleUrls: ['./loans-manager.component.css']
})
export class LoansManagerComponent {
  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | undefined;

  dtOptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject();
  loans$: Subscription = new Subscription();
  loans: Loan[] = [];
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  constructor(private trans: TranslateService, private store: Store<any>) {

  }
  ngOnDestroy(): void {
    this.loans$.unsubscribe()
  }
  ngOnInit(): void {
    this.dtOptions = {
      columnDefs: [
        {
          targets: [0],
          visible: false,
          searchable: false
        }
      ],
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
      this.loans$ = this.store.select(selectPendingLoans).subscribe(val => { this.loans = val; this.updateDataTable() })
      if (this.loans.length == 0) {
        this.store.dispatch(loadPendingLoans({ jwt: this.jwt }))
      }
    }

  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(undefined);

  }
  clickHandler(data: any) {

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
  aprove(id: string) {
    console.log(id);
  }
  deny(id: string) {
    console.log(id)
  }
}
