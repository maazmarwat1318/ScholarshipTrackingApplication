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
declare const grecaptcha: any;
@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent implements OnInit, OnDestroy {
  public loginForm = new FormGroup({
    email: new FormControl<string>(''),
    password: new FormControl<string>(''),
  });
  isFormLoading = signal(false);
  showCaptchaUnverifiedError = signal(false);
  captchaKey = environment.captcha.key;
  captchaToken = '';
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
      'Enter your email and password to login',
    );
    _accountSectionMetaData.sectionTitle.set('Login');
    _pageTitle.setTitle('Login');
  }

  onSubmit = async (event: Event) => {
    event.preventDefault();
    this.loginForm.markAllAsTouched();
    if (this.captchaToken.length === 0) {
      this.showCaptchaUnverifiedError.set(true);
    }
    if (this.loginForm.valid && !this.showCaptchaUnverifiedError()) {
      const requestBody = {
        ...this.loginForm.value,
        captchaToken: this.captchaToken,
      };
      this.isFormLoading.set(true);
      this._httpClient
        .post('https://localhost:7222/Account/Login', requestBody)
        .subscribe(
          (response) => {
            this._userService.onSetAuthCookie((response as any).token);
            this.isFormLoading.set(false);
            this._router.navigateByUrl('/');
          },
          (error) => {
            this.snackbarService.showErrorSnackbar(error.error.message);
            (grecaptcha as any).reset();
            this.isFormLoading.set(false);
          },
        );
    }
  };

  onCaptchaVerified(token: string) {
    this.captchaToken = token;
    this.showCaptchaUnverifiedError.set(false);
  }

  ngOnInit(): void {
    this._scriptLoader.loadScript(environment.captcha.script).then(() => {
      (window as any).onCaptchaVerified = this.onCaptchaVerified.bind(this);
    });
  }

  ngOnDestroy(): void {
    delete (window as any).onCaptchaVerified;
  }
}
