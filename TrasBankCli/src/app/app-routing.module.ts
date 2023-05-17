import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Components/index/index.component';


const routes: Routes = [
  { path: "", component: IndexComponent },
  //{ path: "login", component: LoginComponent },
  { path: "**", component: IndexComponent },
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
