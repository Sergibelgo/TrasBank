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
    FormLoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
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
    EffectsModule.forRoot([UserEffects]),
    StoreDevtoolsModule.instrument({ name:"Test" })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}
