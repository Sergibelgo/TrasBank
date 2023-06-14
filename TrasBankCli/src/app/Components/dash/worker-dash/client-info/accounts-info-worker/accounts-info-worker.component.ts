import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { Account } from '../../../../../Models/Account/account';
import { Store } from '@ngrx/store';
import { selectAccounts } from '../../../../../state/selectors/accounts.selectors';
import { DataTableDirective } from 'angular-datatables';
import { TranslateService } from '@ngx-translate/core';
declare var $: any;
@Component({
  selector: 'app-accounts-info-worker',
  templateUrl: './accounts-info-worker.component.html',
  styleUrls: ['./accounts-info-worker.component.css']
})
export class AccountsInfoWorkerComponent implements OnInit, OnDestroy, AfterViewInit {
  accounts$: Subscription = new Subscription();
  accounts: Account[] = [];


  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective | undefined;

  dtOptions2: DataTables.Settings = {};

  dtTrigger2: Subject<any> = new Subject();
  optionModal: boolean = true;
  idModal: string = "";
  constructor(private store: Store<any>, private trans: TranslateService) {

  }
  ngOnDestroy(): void {
    this.accounts$.unsubscribe();
  }
  ngOnInit(): void {
    this.accounts$ = this.store.select(selectAccounts).subscribe(val => { this.accounts = val;this.updateDataTable() } );
    this.dtOptions2 = {
      order: [[2, "desc"], [1, 'asc']],
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
  modal(check: boolean,id:string) {
    this.optionModal = check;
    this.idModal = id;
    $("#AccountModal").modal("show");
  }
}
