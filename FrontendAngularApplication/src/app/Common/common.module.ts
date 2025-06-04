import { NgModule } from '@angular/core';
import { NameInputComponent } from './Input/name-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PasswordInputComponent } from './Input/password-input.component';
import { EmailInputComponent } from './Input/email-input.component';
import {
  NgComponentOutlet,
  NgFor,
  NgIf,
  NgTemplateOutlet,
} from '@angular/common';
import { InputError } from './Input/input-error.component';
import { SnackbarComponent } from './Alerts/Snackbar/snackbar.component';
import { SnackbarService } from './Alerts/Snackbar/SnackbarService';
import { ActionDialogComponent } from './Alerts/ActionDialog/action-dialog.component';
import { LoadingBtnComponent } from './Buttons/loading-btn.component';
import { ActionDialogService } from './Alerts/ActionDialog/ActionDialogService';
import { ContentDialogComponent } from './Alerts/ContentDialog/content-dialog.component';
import { DegreeSelectComponent } from './Input/degree-select.component';
import { RoleSelectComponent } from './Input/role-select.component';

@NgModule({
  declarations: [
    NameInputComponent,
    PasswordInputComponent,
    EmailInputComponent,
    InputError,
    LoadingBtnComponent,
    SnackbarComponent,
    ActionDialogComponent,
    ContentDialogComponent,
    DegreeSelectComponent,
    RoleSelectComponent,
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
    ContentDialogComponent,
    NgTemplateOutlet,
    DegreeSelectComponent,
    RoleSelectComponent,
  ],
  imports: [ReactiveFormsModule, NgIf, NgFor, NgTemplateOutlet],
  providers: [SnackbarService],
})
export class CommonModule {}
