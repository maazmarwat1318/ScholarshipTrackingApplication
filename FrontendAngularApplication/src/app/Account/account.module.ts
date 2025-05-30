import { NgModule } from '@angular/core';
import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './Login/login.component';
import { AccountLayoutComponent } from './account-layout.component';
import { AccountSectionMetaData } from './State/AccountSectionMetaData';
import { CommonModule } from '../Common/common.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LoginComponent,
    AccountLayoutComponent
  ],
  imports: [
    AccountRoutingModule,
    CommonModule
  ],
  providers: [AccountSectionMetaData]
})
export class AccountModule {
}
