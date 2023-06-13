import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject, Subscription } from 'rxjs';
import { selectActiveLoan, selectLoans } from '../../../../state/selectors/loans.selectors';
import { Loan } from '../../../../Models/loan/loan';
import { DataTableDirective } from 'angular-datatables';
import { TranslateService } from '@ngx-translate/core';
import { setActiveLoan } from '../../../../state/actions/loans.actions';
declare var $: any;

@Component({
  selector: 'app-loans',
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.css']
})
export class LoansComponent {

}
