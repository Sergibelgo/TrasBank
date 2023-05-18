import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

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
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
