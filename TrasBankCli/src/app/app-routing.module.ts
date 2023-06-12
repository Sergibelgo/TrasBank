import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Components/index/index.component';
import { LoginComponent } from './Components/login/login.component';
import { DashBoardComponent } from './Components/dash/dash-board/dash-board.component';
import { LoginWorkerComponent } from './Components/login-worker/login-worker.component';
import { WorkerDashComponent } from './Components/dash/worker-dash/worker-dash.component';


const routes: Routes = [
  { path: "", component: IndexComponent },
  { path: "login", component: LoginComponent },
  { path: "dash", component: DashBoardComponent },
  { path: "loginWorker", component: LoginWorkerComponent },
  { path: "WorkerDash", component: WorkerDashComponent },
  { path: "**", component: IndexComponent },
]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
