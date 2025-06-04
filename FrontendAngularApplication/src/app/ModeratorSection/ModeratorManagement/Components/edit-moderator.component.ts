import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SnackbarService } from '../../../Common/Alerts/Snackbar/SnackbarService';
import { UserService } from '../../../Services/UserService';
import { TitleService } from '../../Service/TitleService';

@Component({
  selector: 'edit-moderator',
  standalone: false,
  template: `
    <div
      class="min-full-height-inside-main-layout d-flex align-items-center justify-content-center"
    >
      <div class="normal-form-width border border-1 rounded-2 p-3">
        <div *ngIf="isFetchingModerator()" class="text-center p-5">
          <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Loading moderator...</span>
          </div>
          <p class="mt-3">Loading moderator data...</p>
        </div>

        <div *ngIf="fetchError()" class="alert alert-danger text-center">
          <p>Error: {{ fetchError() }}</p>
          <button class="btn btn-primary" (click)="onRetryFetch()">
            Retry
          </button>
        </div>

        <ng-container *ngIf="!isFetchingModerator() && !fetchError()">
          <p class="h6 text-center mb-4">Edit Moderator Details</p>
          <form
            [formGroup]="editModeratorForm"
            (ngSubmit)="onSubmit($event)"
            method="post"
          >
            <input type="hidden" formControlName="moderatorId" />

            <div class="mb-2">
              <email-input
                [inputControl]="editModeratorForm.controls.email"
              ></email-input>
            </div>
            <div class="mb-2">
              <name-input
                inputLabel="First Name"
                [inputControl]="editModeratorForm.controls.firstName"
              ></name-input>
            </div>
            <div class="mb-2">
              <name-input
                inputLabel="Last Name"
                [inputControl]="editModeratorForm.controls.lastName"
              ></name-input>
            </div>
            <div class="mb-4">
              <role-select
                [roleControl]="editModeratorForm.controls.role"
              ></role-select>
            </div>

            <div class="d-flex justify-content-center">
              <button
                loading-btn
                class="w-100 btn btn-primary"
                style="max-width: 250px"
                btnType="submit"
                [disabled]="isFormLoading() || editModeratorForm.invalid"
                [isLoading]="isFormLoading()"
                (click)="onSubmit($event)"
              >
                Update
              </button>
            </div>
          </form>
        </ng-container>
      </div>
    </div>
  `,
})
export class EditModeratorComponent implements OnInit, OnDestroy {
  isFetchingModerator = signal(true);
  fetchError = signal<any>(null);
  moderatorId!: number;
  private routeSubscription: Subscription | undefined;

  public editModeratorForm = new FormGroup({
    moderatorId: new FormControl<any>(0, Validators.required),
    email: new FormControl<any>('', [Validators.required, Validators.email]),
    firstName: new FormControl<any>('', Validators.required),
    lastName: new FormControl<any>('', Validators.required),
    role: new FormControl<any>(null, Validators.required),
  });

  isFormLoading = signal(false);

  constructor(
    private _titleService: TitleService,
    private _httpClient: HttpClient,
    public snackbarService: SnackbarService,
    private _userService: UserService,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
  ) {
    this._titleService.setTitle('Edit Moderator');
  }

  ngOnInit(): void {
    this.routeSubscription = this._activatedRoute.paramMap.subscribe(
      (params) => {
        const id = params.get('id');
        if (id) {
          this.moderatorId = +id;
          this.fetchModeratorData();
        } else {
          this.fetchError.set('Moderator ID not provided in URL.');
          this.isFetchingModerator.set(false);
        }
      },
    );
  }

  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
  }

  fetchModeratorData(): void {
    this.isFetchingModerator.set(true);
    this.fetchError.set(null);

    let headers = new HttpHeaders();
    if (this._userService.authToken) {
      headers = headers.set(
        'Authorization',
        `Bearer ${this._userService.authToken}`,
      );
    }

    this._httpClient
      .get<any>(
        `https://localhost:7222/ScholarshipModerator/${this.moderatorId}`,
        {
          headers,
        },
      )
      .subscribe({
        next: (moderatorData) => {
          this.editModeratorForm.patchValue({
            moderatorId: moderatorData.id,
            email: moderatorData.email,
            firstName: moderatorData.firstName,
            lastName: moderatorData.lastName,
            role: moderatorData.role,
          });
          this.isFetchingModerator.set(false);
        },
        error: (error) => {
          console.error('Error fetching moderator:', error);
          this.fetchError.set(
            error.error?.message || 'Failed to fetch moderator data.',
          );
          this.isFetchingModerator.set(false);
          this.snackbarService.showErrorSnackbar(
            'Failed to load moderator data.',
          );
        },
      });
  }

  onRetryFetch(): void {
    this.fetchModeratorData();
  }

  onSubmit = async (event: Event) => {
    event.preventDefault();
    this.editModeratorForm.markAllAsTouched();
    if (this.editModeratorForm.valid) {
      let headers = new HttpHeaders();
      if (this._userService.authToken) {
        headers = headers.set(
          'Authorization',
          `Bearer ${this._userService.authToken}`,
        );
      }
      this.isFormLoading.set(true);

      const payload: any = {
        moderatorId: this.editModeratorForm.controls.moderatorId.value,
        email: this.editModeratorForm.controls.email.value,
        firstName: this.editModeratorForm.controls.firstName.value,
        lastName: this.editModeratorForm.controls.lastName.value,
        role: this.editModeratorForm.controls.role.value,
      };

      this._httpClient
        .post(`https://localhost:7222/ScholarshipModerator/Edit`, payload, {
          headers,
        })
        .subscribe({
          next: (response) => {
            this.isFormLoading.set(false);
            this.snackbarService.showSuccessSnackbar(
              'Moderator updated successfully!',
            );
          },
          error: (error) => {
            this.isFormLoading.set(false);
            this.snackbarService.showErrorSnackbar(
              error.error?.message ?? 'Unknown error occurred during update.',
            );
          },
        });
    }
  };
}
