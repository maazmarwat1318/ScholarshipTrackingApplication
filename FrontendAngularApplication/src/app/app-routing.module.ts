import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGaurd } from './Gaurds/AuthenticationGaurd';
import { AppComponent } from './app.component';

const routes: Routes = [
  {path: "", canActivate: [AuthenticationGaurd], component: AppComponent},
  {
    path: "account",
    loadChildren: () => import('./Account/account.module').then(m => m.AccountModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
