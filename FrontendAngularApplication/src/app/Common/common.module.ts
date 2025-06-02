import { NgModule } from '@angular/core';
import { NameInputComponent } from './Input/name-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PasswordInputComponent } from './Input/password-input.component';
import { EmailInputComponent } from './Input/email-input.component';
import { NgIf } from '@angular/common';
import { InputError } from './Input/input-error.component';
import { PrimaryBtnComponent } from './Buttons/primary-btn.component';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { SnackbarComponent } from './Alerts/Snackbar/snackbar.component';
import { SnackbarService } from './Alerts/Snackbar/SnackbarService';

@NgModule({
  declarations: [
    NameInputComponent,
    PasswordInputComponent,
    EmailInputComponent,
    InputError,
    PrimaryBtnComponent,
    SnackbarComponent,
  ],
  exports: [
    NameInputComponent,
    ReactiveFormsModule,
    PasswordInputComponent,
    EmailInputComponent,
    NgIf,
    PrimaryBtnComponent,
    SnackbarComponent,
  ],
  imports: [ReactiveFormsModule, NgIf],
  providers: [SnackbarService],
})
export class CommonModule {}
