import { Component, output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'student-search-field',
  standalone: false,
  template: `
    <form
      [formGroup]="searchForm"
      style="max-width: 300px"
      (ngSubmit)="onSearch($event)"
    >
      <div class="input-group">
        <input
          [formControl]="searchForm.controls.searchString"
          type="text"
          [class.is-invalid]="
            !searchForm.controls.searchString.valid &&
            searchForm.controls.searchString.touched
          "
          class="form-control"
          asp-for="SearchString"
          placeholder="Search via name"
        />
        <button class="btn btn-primary" type="submit">
          <i class=" bi-search"></i>
        </button>
      </div>
    </form>
  `,
})
export class StudentSearchFieldComponent {
  searchForm = new FormGroup({
    searchString: new FormControl('', { validators: Validators.required }),
  });

  searchClicked = output<string>();

  onSearch(event: Event) {
    event.preventDefault();
    this.searchForm.markAllAsTouched();
    if (this.searchForm.valid) {
      this.searchClicked.emit(this.searchForm.value.searchString ?? '');
    }
  }
}
