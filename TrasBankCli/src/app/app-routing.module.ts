import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Components/index/index.component';
import { LoginComponent } from './Components/login/login.component';
import { DashBoardComponent } from './Components/dash/dash-board/dash-board.component';


const routes: Routes = [
  { path: "", component: IndexComponent },
  { path: "login", component: LoginComponent },
  { path: "dash", component: DashBoardComponent },
  { path: "**", component: IndexComponent },
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
