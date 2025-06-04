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

declare const grecaptcha: any;
@Component({
  selector: 'login',
  template: `<div
    class="min-full-height-inside-main-layout d-flex align-items-center justify-content-center"
  >
    <div class="normal-form-width border border-1 rounded-2 p-3">
      <p class="h6 text-center mb-4">Enter details to create a student</p>
      <form
        [formGroup]="createStudentForm"
        (ngSubmit)="onSubmit($event)"
        method="post"
      >
        <div class="mb-2">
          <email-input
            [inputControl]="createStudentForm.controls.email"
          ></email-input>
        </div>
        <div class="mb-2">
          <name-input
            inputLabel="First Name"
            [inputControl]="createStudentForm.controls.firstName"
          ></name-input>
        </div>
        <div class="mb-2">
          <name-input
            inputLabel="Last Name"
            [inputControl]="createStudentForm.controls.lastName"
          ></name-input>
        </div>
        <div class="mb-2">
          <degree-select
            [degreeControl]="createStudentForm.controls.degreeId"
          />
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
export class CreateStudentComponent {
  public createStudentForm = new FormGroup({
    email: new FormControl<string>(''),
    firstName: new FormControl<string>(''),
    lastName: new FormControl<string>(''),
    degreeId: new FormControl<number | null>({ value: null, disabled: true }),
  });
  isFormLoading = signal(false);
  constructor(
    private _titleService: TitleService,
    private _httpClient: HttpClient,
    public snackbarService: SnackbarService,
    private _userService: UserService,
  ) {
    this._titleService.setTitle('Create Student');
  }

  onSubmit = async (event: Event) => {
    event.preventDefault();
    this.createStudentForm.markAllAsTouched();
    if (this.createStudentForm.valid) {
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
          'https://localhost:7222/Student/Create',
          this.createStudentForm.value,
          { headers },
        )
        .subscribe(
          (response) => {
            this.isFormLoading.set(false);
            this.snackbarService.showSuccessSnackbar(
              'Student created successfuly',
            );
            this.createStudentForm.reset();
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
