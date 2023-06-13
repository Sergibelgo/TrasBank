import { Component, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { User } from '../../../../Models/User/user';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { selectJWT } from '../../../../state/selectors/user.selectors';
import { Loan } from '../../../../Models/loan/loan';
import { selectCustomers, selectPendingLoans } from '../../../../state/selectors/worker.selectors';
import { loadPendingLoans, setActiveUser } from '../../../../state/actions/worker.actions';
import { setUser } from '../../../../state/actions/auth.actions';
import { setIndex } from '../../../../state/actions/utils.actions';
import Swal from 'sweetalert2';
import { LoansService } from '../../../../services/loans.service';
import { errorAlert, successAlert } from '../../../Utils';

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
  users$: Subscription = new Subscription();
  users: User[] = [];
  jwt$: Subscription = new Subscription();
  jwt: string = "";
  constructor(private trans: TranslateService, private store: Store<any>, private service: LoansService) {

  }
  ngOnDestroy(): void {
    this.loans$.unsubscribe()
  }
  ngOnInit(): void {
    this.dtOptions = {
      order: [[2, "asc"], [1, 'asc']],
      responsive: true,
      lengthChange: false,
      pageLength: 10,
      language: {
        url: this.trans.currentLang == "es" ? `assets/i18n/datatables.es.json` : `assets/i18n/datatables.en.json`
      },
      rowCallback: (row: Node, data: any[] | Object, index: number) => {
        const self = this;
        $('td:not(.notClick)', row).off('click');
        $('td:not(.notClick)', row).on('click', () => {
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
    this.users$ = this.store.select(selectCustomers).subscribe(val => this.users = val)

  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(undefined);

  }
  clickHandler(data: any) {
    this.store.dispatch(setActiveUser({ id: data[2] }))
    this.store.dispatch(setUser({ user: this.users.find(val => val.Id == data[2]) as User }))
    this.store.dispatch(setIndex({ index: 1 }))
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
  submit(id: string, check: boolean) {
    Swal.fire({
      icon: "info",
      showCancelButton: true,
      text: this.trans.instant("Are you sure of the operation") + "?"
    }).then(val => {
      if (val.isConfirmed) {
        this.service.changeLoanStatus(this.jwt, id, check)
          .then(val => {
            successAlert(this.trans.instant("Loan modified successfully"))
            this.store.dispatch(loadPendingLoans({ jwt: this.jwt }))
          })
          .catch(val => {
            errorAlert(val)
          })
      }
    })
  }
}
