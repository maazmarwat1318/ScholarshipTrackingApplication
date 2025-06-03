import { NgModule } from '@angular/core';
import { NameInputComponent } from './Input/name-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PasswordInputComponent } from './Input/password-input.component';
import { EmailInputComponent } from './Input/email-input.component';
import { NgFor, NgIf } from '@angular/common';
import { InputError } from './Input/input-error.component';
import { SnackbarComponent } from './Alerts/Snackbar/snackbar.component';
import { SnackbarService } from './Alerts/Snackbar/SnackbarService';
import { ActionDialogComponent } from './Alerts/ActionDialog/action-dialog.component';
import { LoadingBtnComponent } from './Buttons/loading-btn.component';
import { ActionDialogService } from './Alerts/ActionDialog/ActionDialogService';

@NgModule({
  declarations: [
    NameInputComponent,
    PasswordInputComponent,
    EmailInputComponent,
    InputError,
    LoadingBtnComponent,
    SnackbarComponent,
    ActionDialogComponent,
  ],
  exports: [
    NameInputComponent,
    ReactiveFormsModule,
    PasswordInputComponent,
    EmailInputComponent,
    InputError,
    NgIf,
    LoadingBtnComponent,
    SnackbarComponent,
    ActionDialogComponent,
    NgFor,
  ],
  imports: [ReactiveFormsModule, NgIf, NgFor],
  providers: [SnackbarService],
})
export class CommonModule {}
