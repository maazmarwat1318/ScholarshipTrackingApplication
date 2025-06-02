import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
  signal,
} from '@angular/core';
import { AccountSectionMetaData } from '../State/AccountSectionMetaData';
import { Title } from '@angular/platform-browser';
import { FormControl, FormGroup, FormSubmittedEvent } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { ScriptLoaderService } from '../../Services/ScriptLoader';
import { SnackbarService } from '../../Common/Alerts/Snackbar/SnackbarService';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { UserService } from '../../Services/UserService';
@Component({
  selector: 'login',
  templateUrl: './forgot-password.component.html',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ForgotPasswordComponent {
  public forgotPasswordForm = new FormGroup({
    email: new FormControl<string>(''),
  });
  isFormLoading = signal(false);
  constructor(
    private _accountSectionMetaData: AccountSectionMetaData,
    private _pageTitle: Title,
    private _httpClient: HttpClient,
    private _scriptLoader: ScriptLoaderService,
    public snackbarService: SnackbarService,
    private _router: Router,
    private _userService: UserService,
  ) {
    _accountSectionMetaData.sectionSubtitle.set(
      'Enter your email to get a reset password email',
    );
    _accountSectionMetaData.sectionTitle.set('Forgot Password');
    _pageTitle.setTitle('Forgot Password');
  }

  onSubmit = async (event: Event) => {
    event.preventDefault();
    this.forgotPasswordForm.markAllAsTouched();
    if (this.forgotPasswordForm.valid) {
      this.isFormLoading.set(true);
      this._httpClient
        .post(
          'https://localhost:7222/Account/ForgotPassword',
          this.forgotPasswordForm.value,
        )
        .subscribe(
          (response) => {
            this.snackbarService.showSuccessSnackbar(
              'A reset password email has been sent successfuly at your address. Please reset your password and log in',
            );
            this._router.navigateByUrl('account/login');
          },
          (error) => {
            this.snackbarService.showErrorSnackbar(error.error.message);
            this.isFormLoading.set(false);
          },
        );
    }
  };
}
