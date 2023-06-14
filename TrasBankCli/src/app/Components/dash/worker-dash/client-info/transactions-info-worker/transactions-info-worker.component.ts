import { Component, ViewChild } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { Transaction } from '../../../../../Models/Transaction/transaction';
import { DataTableDirective } from 'angular-datatables';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { selectTransactions } from '../../../../../state/selectors/accounts.selectors';

@Component({
  selector: 'app-transactions-info-worker',
  templateUrl: './transactions-info-worker.component.html',
  styleUrls: ['./transactions-info-worker.component.css']
})
export class TransactionsInfoWorkerComponent {
  transactions$: Subscription = new Subscription();
  transactions: Transaction[] = [];


  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | undefined;

  dtOptions2: DataTables.Settings = {};

  dtTrigger2: Subject<any> = new Subject();
  constructor(private store: Store<any>, private trans: TranslateService) {

  }
  ngOnDestroy(): void {
    this.transactions$.unsubscribe();
  }
  ngOnInit(): void {
    this.transactions$ = this.store.select(selectTransactions).subscribe(val => { this.transactions = val.map(val => { return { ...val, Date: new Date(val.Date) } }); this.updateDataTable() });
    this.dtOptions2 = {
      columnDefs: [{

        targets: 3,
        type: "date"
      }],
      order: [[3, "desc"], [1, 'desc']],
      responsive: true,
      lengthChange: false,
      pageLength: 5,
      language: {
        url: this.trans.currentLang == "es" ? `assets/i18n/datatables.es.json` : `assets/i18n/datatables.en.json`
      },
    }

  }
  ngAfterViewInit(): void {

    this.dtTrigger2.next(undefined);

  }
  updateDataTable() {
    this.dtElement?.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Change Language
      this.dtOptions2.language = { url: this.trans.currentLang == "es" ? `assets/i18n/datatables.es.json` : `assets/i18n/datatables.en.json` }
      // Call the dtTrigger to rerender again
      this.dtTrigger2.next(undefined);
    });
  }
}
