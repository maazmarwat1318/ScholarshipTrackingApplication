import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, computed, input, OnInit, signal } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { UserService } from '../../Services/UserService';

@Component({
  selector: 'degree-select',
  standalone: false,
  template: `
    <div>
      <label [for]="selectFor()" class="form-label">
        {{ selectLabel() }}
        <span *ngIf="required()" class="text-danger">*</span>
      </label>
      <select
        [id]="selectFor()"
        [formControl]="degreeControl()"
        class="form-select"
        [class.is-invalid]="!degreeControl().valid && degreeControl().touched"
      >
        <option [value]="null" disabled>{{ selectPlaceholder() }}</option>
        <option *ngIf="isLoading()">Loading degrees...</option>
        <option *ngIf="hasError()">Error fetching degrees!</option>
        <ng-container *ngIf="!isLoading() && !hasError()">
          <option *ngFor="let degree of degrees()" [value]="degree.id">
            {{ degree.degreeTitle }}
          </option>
        </ng-container>
      </select>
      <input-error
        [errorMessagesDictionary]="errorMessages"
        [inputControl]="degreeControl()"
      />
    </div>
  `,
})
export class DegreeSelectComponent implements OnInit {
  degreeControl = input.required<FormControl<number | null>>();
  selectFor = input('degree');
  selectLabel = input('Degree');
  selectPlaceholder = input('-- Select a Degree --');
  required = input(true);

  degrees = signal<{ id: string; degreeTitle: string }[]>([]);
  isLoading = signal(false);
  hasError = signal(false);

  constructor(
    private _httpClient: HttpClient,
    private _userService: UserService,
  ) {}

  errorMessages = {
    required: `${this.selectLabel()} is required`,
  };

  ngOnInit(): void {
    const ctrl = this.degreeControl();
    if (this.required()) {
      ctrl.setValidators([Validators.required]);
    }
    ctrl.updateValueAndValidity();

    this.fetchDegrees();
  }

  fetchDegrees(): void {
    this.isLoading.set(true);
    this.hasError.set(false);

    let headers = new HttpHeaders();
    if (this._userService.authToken) {
      headers = headers.set(
        'Authorization',
        `Bearer ${this._userService.authToken}`,
      );
    }

    this._httpClient
      .get<
        { id: string; degreeTitle: string }[]
      >('https://localhost:7222/Degree', { headers })
      .subscribe(
        (response) => {
          this.degrees.set(response);
          this.isLoading.set(false);
          this.degreeControl().enable();
        },
        (err) => {
          this.hasError.set(true);
          this.isLoading.set(false);
        },
      );
  }
}
