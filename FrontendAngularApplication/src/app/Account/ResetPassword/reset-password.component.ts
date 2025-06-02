import {
  ChangeDetectionStrategy,
  Component,
  input,
  OnDestroy,
  OnInit,
  signal,
} from '@angular/core';
import { AccountSectionMetaData } from '../State/AccountSectionMetaData';
import { Title } from '@angular/platform-browser';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ScriptLoaderService } from '../../Services/ScriptLoader';
import { SnackbarService } from '../../Common/Alerts/Snackbar/SnackbarService';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../Services/UserService';
@Component({
  selector: 'reset-password',
  templateUrl: './reset-password.component.html',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ResetPasswordComponent {
  public resetPasswordForm = new FormGroup({
    newPassword: new FormControl<string>(''),
    confirmPassword: new FormControl<string>(''),
  });
  isFormLoading = signal(false);
  token = '';
  constructor(
    private _accountSectionMetaData: AccountSectionMetaData,
    private _pageTitle: Title,
    private _httpClient: HttpClient,
    public snackbarService: SnackbarService,
    private _router: Router,
    private _route: ActivatedRoute,
  ) {
    _accountSectionMetaData.sectionSubtitle.set('Enter your new password');
    _accountSectionMetaData.sectionTitle.set('Reset Password');
    _pageTitle.setTitle('Reset Password');
    this.setToken();
  }

  onSubmit = async (event: Event) => {
    event.preventDefault();
    this.resetPasswordForm.markAllAsTouched();
    if (this.resetPasswordForm.valid) {
      this.isFormLoading.set(true);
      this._httpClient
        .post('https://localhost:7222/Account/ResetPassword', {
          ...this.resetPasswordForm.value,
          token: this.token,
        })
        .subscribe(
          (response) => {
            this.snackbarService.showSuccessSnackbar(
              'Your password has been reset. Please login',
            );
            this._router.navigateByUrl('account/login');
          },
          (error) => {
            this.snackbarService.showErrorSnackbar(error.error.message);
            this._router.navigateByUrl('/account/forgot-password');
            this.isFormLoading.set(false);
          },
        );
    }
  };

  setToken() {
    const tokenFromQuery = this._route.snapshot.queryParamMap.get('token');
    this.token = tokenFromQuery ?? '';
    if (this.token.length === 0) {
      this.snackbarService.showErrorSnackbar(
        'This url is invalid. Please request a new one.',
      );
      this._router.navigateByUrl('/account/forgot-password');
    }
  }
}
