import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SnackbarService } from '../../../Common/Alerts/Snackbar/SnackbarService';
import { UserService } from '../../../Services/UserService';
import { TitleService } from '../../Service/TitleService';

@Component({
  selector: 'edit-student',
  standalone: false,
  template: `
    <div
      class="min-full-height-inside-main-layout d-flex align-items-center justify-content-center"
    >
      <div class="normal-form-width border border-1 rounded-2 p-3">
        <div *ngIf="isFetchingStudent()" class="text-center p-5">
          <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Loading student...</span>
          </div>
          <p class="mt-3">Loading student data...</p>
        </div>

        <div *ngIf="fetchError()" class="alert alert-danger text-center">
          <p>Error: {{ fetchError() }}</p>
          <button class="btn btn-primary" (click)="onRetryFetch()">
            Retry
          </button>
        </div>

        <ng-container *ngIf="!isFetchingStudent() && !fetchError()">
          <p class="h6 text-center mb-4">Edit Student Details</p>
          <form
            [formGroup]="editStudentForm"
            (ngSubmit)="onSubmit($event)"
            method="post"
          >
            <input type="hidden" formControlName="studentId" />

            <div class="mb-2">
              <email-input
                [inputControl]="editStudentForm.controls.email"
              ></email-input>
            </div>
            <div class="mb-2">
              <name-input
                inputLabel="First Name"
                [inputControl]="editStudentForm.controls.firstName"
              ></name-input>
            </div>
            <div class="mb-2">
              <name-input
                inputLabel="Last Name"
                [inputControl]="editStudentForm.controls.lastName"
              ></name-input>
            </div>
            <div class="mb-2">
              <degree-select
                [degreeControl]="editStudentForm.controls.degreeId"
              ></degree-select>
            </div>

            <div class="d-flex justify-content-center">
              <button
                loading-btn
                class="w-100 btn btn-primary"
                style="max-width: 250px"
                btnType="submit"
                [disabled]="isFormLoading() || editStudentForm.invalid"
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
export class EditStudentComponent implements OnInit, OnDestroy {
  isFetchingStudent = signal(true);
  fetchError = signal<any>(null);
  studentId!: number;
  private routeSubscription: Subscription | undefined;

  public editStudentForm = new FormGroup({
    studentId: new FormControl<any>(0, Validators.required),
    email: new FormControl<any>('', [Validators.required, Validators.email]),
    firstName: new FormControl<any>('', Validators.required),
    lastName: new FormControl<any>('', Validators.required),
    degreeId: new FormControl<any>(null, Validators.required),
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
    this._titleService.setTitle('Edit Student');
  }

  ngOnInit(): void {
    this.routeSubscription = this._activatedRoute.paramMap.subscribe(
      (params) => {
        const id = params.get('id');
        if (id) {
          this.studentId = +id;
          this.fetchStudentData();
        } else {
          this.fetchError.set('Student ID not provided in URL.');
          this.isFetchingStudent.set(false);
        }
      },
    );
  }

  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
  }

  fetchStudentData(): void {
    this.isFetchingStudent.set(true);
    this.fetchError.set(null);

    let headers = new HttpHeaders();
    if (this._userService.authToken) {
      headers = headers.set(
        'Authorization',
        `Bearer ${this._userService.authToken}`,
      );
    }

    this._httpClient
      .get<any>(`https://localhost:7222/Student/${this.studentId}`, { headers })
      .subscribe({
        next: (studentData) => {
          this.editStudentForm.patchValue({
            studentId: studentData.id,
            email: studentData.email,
            firstName: studentData.firstName,
            lastName: studentData.lastName,
            degreeId: studentData.degreeId,
          });
          this.isFetchingStudent.set(false);
        },
        error: (error) => {
          console.error('Error fetching student:', error);
          this.fetchError.set(
            error.error?.message || 'Failed to fetch student data.',
          );
          this.isFetchingStudent.set(false);
          this.snackbarService.showErrorSnackbar(
            'Failed to load student data.',
          );
        },
      });
  }

  onRetryFetch(): void {
    this.fetchStudentData();
  }

  onSubmit = async (event: Event) => {
    event.preventDefault();
    this.editStudentForm.markAllAsTouched();
    if (this.editStudentForm.valid) {
      let headers = new HttpHeaders();
      if (this._userService.authToken) {
        headers = headers.set(
          'Authorization',
          `Bearer ${this._userService.authToken}`,
        );
      }
      this.isFormLoading.set(true);

      const payload: any = {
        studentId: this.editStudentForm.controls.studentId.value,
        email: this.editStudentForm.controls.email.value,
        firstName: this.editStudentForm.controls.firstName.value,
        lastName: this.editStudentForm.controls.lastName.value,
        degreeId: this.editStudentForm.controls.degreeId.value,
      };

      this._httpClient
        .post(`https://localhost:7222/Student/Edit`, payload, { headers })
        .subscribe({
          next: (response) => {
            this.isFormLoading.set(false);
            this.snackbarService.showSuccessSnackbar(
              'Student updated successfully!',
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
