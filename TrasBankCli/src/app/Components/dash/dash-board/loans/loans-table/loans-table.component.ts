import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { Loan } from '../../../../../Models/loan/loan';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { selectLoans } from '../../../../../state/selectors/loans.selectors';
import { setActiveLoan } from '../../../../../state/actions/loans.actions';
declare var $: any;

@Component({
  selector: 'app-loans-table',
  templateUrl: './loans-table.component.html',
  styleUrls: ['./loans-table.component.css']
})
export class LoansTableComponent implements OnInit, OnDestroy, AfterViewInit {
  loans$: Subscription = new Subscription();
  loans: Loan[] = [];

  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | undefined;

  dtOptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject();
  @Input() checkW: string | null = null;
  constructor(private store: Store<any>, private trans: TranslateService) {

  }
  ngOnDestroy(): void {
    this.loans$.unsubscribe();
  }
  ngOnInit(): void {
    this.loans$ = this.store.select(selectLoans).subscribe(val => { this.loans = val; this.updateDataTable() });
    this.dtOptions = {
      columnDefs: [
        {
          targets: [0],
          visible: false,
          searchable: false
        }
      ],
      order: [[3, "desc"], [2, 'desc']],
      responsive: true,
      lengthChange: false,
      pageLength: 10,
      language: {
        url: this.trans.currentLang == "es" ? `assets/i18n/datatables.es.json` : `assets/i18n/datatables.en.json`
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
  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(undefined);

  }
  clickHandler(data: any) {
    this.store.dispatch(setActiveLoan({ id: data[0] }));
    $("#ModalActiveLoan").modal("show");
  }
  updateDataTable() {
    this.dtElement?.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Change Language
      this.dtOptions.language = { url: this.trans.currentLang == "es" ? `assets/i18n/datatables.es.json` : `assets/i18n/datatables.en.json` }
      // Call the dtTrigger to rerender again
      this.dtTrigger.next(undefined);
    });
  }
}
