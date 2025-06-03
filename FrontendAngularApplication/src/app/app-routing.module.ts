import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGaurd } from './Gaurds/AuthenticationGaurd';
import { AppComponent } from './app.component';
import { HomeComponent } from './SuperModerator/home-component';
import { HomeNavigationReroute } from './Gaurds/HomeNavigationReroute';
import { ModeratorAuthGaurd } from './Gaurds/ModeratorAuthGaurd';
import { NotFoundComponent } from './notfound.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthenticationGaurd, HomeNavigationReroute],
    component: HomeComponent,
  },
  {
    path: 'account',
    loadChildren: () =>
      import('./Account/account.module').then((m) => m.AccountModule),
  },
  {
    path: 'moderator',
    canActivate: [AuthenticationGaurd, ModeratorAuthGaurd],
    loadChildren: () =>
      import('./ModeratorSection/moderator-section.module').then(
        (m) => m.ModeratorSectionModule,
      ),
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { bindToComponentInputs: true }),
    HomeComponent,
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
