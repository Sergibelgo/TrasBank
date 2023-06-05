import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject, Subscription } from 'rxjs';
import { selectActiveLoan, selectLoans } from '../../../../state/selectors/loans.selectors';
import { Loan } from '../../../../Models/loan/loan';
import { DataTableDirective } from 'angular-datatables';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-loans',
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.css']
})
export class LoansComponent implements OnInit, OnDestroy, AfterViewInit {
  
  loans$: Subscription = new Subscription();
  loans: Loan[] = [];
  activeLoan$: Subscription = new Subscription();
  activeLoan: Loan = {
    Ammount: 0,
    CustomerId: "",
    CustomerName: "",
    EndDate: new Date(),
    Id: "",
    InterestRate: 0,
    LoanStatus: "",
    LoanType: "",
    RemainingAmmount: 0,
    RemainingInstallments: 0,
    StartDate: new Date(),
    TotalInstallments: 0
  }
  option: boolean =false;
  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | undefined;

  dtOptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject();
  constructor(private store: Store<any>, private trans: TranslateService) {

  }
  ngOnDestroy(): void {
    this.loans$.unsubscribe();
    this.activeLoan$.unsubscribe();
  }
  ngOnInit(): void {
    this.loans$ = this.store.select(selectLoans).subscribe(val => this.loans = val);
    this.activeLoan$ = this.store.select(selectActiveLoan).subscribe(val => this.activeLoan = val);
    this.dtOptions = {
      columnDefs: [
        {
          targets: [0],
          visible: false,
          searchable: false
        }
      ],
      order: [[3, "asc"], [2, 'desc']],
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
  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(undefined);

  }
  clickHandler(data:any) {

  }
  changeStatus(check: boolean) {
    this.option = check;
    if (!check) {
      this.updateDataTable();
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
