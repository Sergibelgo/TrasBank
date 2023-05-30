import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { IndexComponent } from './Components/index/index.component';
import { FooterComponent } from './Components/footer/footer.component';
import { ScrollTopComponent } from './Components/scroll-top/scroll-top.component';
import { HeroComponent } from './Components/index/hero/hero.component';
import { BtnRegisterComponent } from './Components/index/btn-register/btn-register.component';
import { FeaturesHolderComponent } from './Components/index/features-holder/features-holder.component';
import { FeatureComponent } from './Components/index/feature/feature.component';
import { AboutComponent } from './Components/index/about/about.component';
import { UsComponent } from './Components/index/us/us.component';
import { FeatureExtendedComponent } from './Components/index/feature-extended/feature-extended.component';
import { LoginComponent } from './Components/login/login.component';
import { FormLoginComponent } from './Components/login/form-login/form-login.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { userReducer } from './state/reducers/user.reducer';
import { ROOT_REDUCERS } from './state/app.state';
import { UserEffects } from './state/effects/user.effects';
import { FormRegisterComponent } from './Components/login/form-register/form-register.component';
import { EnumsEffects } from './state/effects/enums.effects';
import { WorkerEffects } from './state/effects/worker.effects';
import { DashBoardComponent } from './Components/dash/dash-board/dash-board.component';
import { MenuLComponent } from './Components/dash/dash-board/menu-l/menu-l.component';
import { MenuSComponent } from './Components/dash/dash-board/menu-s/menu-s.component';
import { AccountsInfoComponent } from './Components/dash/dash-board/accounts-info/accounts-info.component';
import { FullCalendarModule } from '@fullcalendar/angular';
import { LogoutButtonComponent } from './Components/dash/dash-board/logout-button/logout-button.component';
import { DropdownMenuComponent } from './Components/dash/dash-board/menu-l/dropdown-menu/dropdown-menu.component';
import { AccountsEffects } from './state/effects/accounts.effects';
import { ModalTransactionComponent } from './Components/dash/dash-board/accounts-info/modal-transaction/modal-transaction.component';
import { TransactionsMakeComponent } from './Components/dash/dash-board/transactions-make/transactions-make.component';
import { LoansMakeComponent } from './Components/dash/dash-board/loans-make/loans-make.component';
import { MessagesComponent } from './Components/dash/dash-board/messages/messages.component';
import { ProfileComponent } from './Components/dash/dash-board/profile/profile.component';


@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    FooterComponent,
    ScrollTopComponent,
    HeroComponent,
    BtnRegisterComponent,
    FeaturesHolderComponent,
    FeatureComponent,
    AboutComponent,
    UsComponent,
    FeatureExtendedComponent,
    LoginComponent,
    FormLoginComponent,
    FormRegisterComponent,
    DashBoardComponent,
    MenuLComponent,
    MenuSComponent,
    AccountsInfoComponent,
    LogoutButtonComponent,
    DropdownMenuComponent,
    ModalTransactionComponent,
    TransactionsMakeComponent,
    LoansMakeComponent,
    MessagesComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    FullCalendarModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    StoreModule.forRoot(ROOT_REDUCERS),
    EffectsModule.forRoot([UserEffects, EnumsEffects, WorkerEffects, AccountsEffects]),
    StoreDevtoolsModule.instrument({ name:"Test" })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}
