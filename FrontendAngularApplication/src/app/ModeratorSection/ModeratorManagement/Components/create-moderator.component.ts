import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
  signal,
} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FormControl, FormGroup, FormSubmittedEvent } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AccountSectionMetaData } from '../../../Account/State/AccountSectionMetaData';
import { SnackbarService } from '../../../Common/Alerts/Snackbar/SnackbarService';
import { UserService } from '../../../Services/UserService';
import { TitleService } from '../../Service/TitleService';
import { Role } from '../../../Common/enums';

declare const grecaptcha: any;
@Component({
  selector: 'login',
  template: `<div
    class="min-full-height-inside-main-layout d-flex align-items-center justify-content-center"
  >
    <div class="normal-form-width border border-1 rounded-2 p-3">
      <p class="h6 text-center mb-4">Enter details to create a moderator</p>
      <form
        [formGroup]="createModeratorForm"
        (ngSubmit)="onSubmit($event)"
        method="post"
      >
        <div class="mb-2">
          <email-input
            [inputControl]="createModeratorForm.controls.email"
          ></email-input>
        </div>
        <div class="mb-2">
          <name-input
            inputLabel="First Name"
            [inputControl]="createModeratorForm.controls.firstName"
          ></name-input>
        </div>
        <div class="mb-2">
          <name-input
            inputLabel="Last Name"
            [inputControl]="createModeratorForm.controls.lastName"
          ></name-input>
        </div>
        <div class="mb-4">
          <role-select [roleControl]="createModeratorForm.controls.role" />
        </div>

        <div class="d-flex justify-content-center">
          <button
            loading-btn
            class="w-100 btn btn-primary"
            style="max-width: 250px"
            btnType="submit"
            [disabled]="isFormLoading()"
            [isLoading]="isFormLoading()"
            (click)="onSubmit($event)"
          >
            Create
          </button>
        </div>
      </form>
    </div>
  </div>`,
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateModeratorComponent {
  public createModeratorForm = new FormGroup({
    email: new FormControl<string>(''),
    firstName: new FormControl<string>(''),
    lastName: new FormControl<string>(''),
    role: new FormControl<Role | null>(null),
  });
  isFormLoading = signal(false);
  constructor(
    private _titleService: TitleService,
    private _httpClient: HttpClient,
    public snackbarService: SnackbarService,
    private _userService: UserService,
  ) {
    this._titleService.setTitle('Create Moderator');
  }

  onSubmit = async (event: Event) => {
    event.preventDefault();
    this.createModeratorForm.markAllAsTouched();
    if (this.createModeratorForm.valid) {
      let headers = new HttpHeaders();
      if (this._userService.authToken) {
        headers = headers.set(
          'Authorization',
          `Bearer ${this._userService.authToken}`,
        );
      }
      this.isFormLoading.set(true);
      this._httpClient
        .post(
          'https://localhost:7222/ScholarshipModerator/Create',
          this.createModeratorForm.value,
          { headers },
        )
        .subscribe(
          (response) => {
            this.isFormLoading.set(false);
            this.snackbarService.showSuccessSnackbar(
              'Moderator created successfuly',
            );
            this.createModeratorForm.reset();
          },
          (error) => {
            this.snackbarService.showErrorSnackbar(
              error.error?.message ?? 'Unknown error occured',
            );
            this.isFormLoading.set(false);
          },
        );
    }
  };
}
