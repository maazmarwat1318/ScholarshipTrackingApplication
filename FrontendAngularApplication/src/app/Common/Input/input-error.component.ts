import {
  Component,
  computed,
  DoCheck,
  effect,
  Input,
  input,
  OnChanges,
  OnInit,
  Signal,
  signal,
  SimpleChanges,
} from '@angular/core';
import { FormControl, FormControlStatus, Validators } from '@angular/forms';

@Component({
  selector: 'input-error',
  standalone: false,
  template: `
    <div *ngIf="inputControl.invalid && inputControl.touched">
      <span class="invalid-feedback" style="display: inline-block;">{{
        getErrorMessage()
      }}</span>
    </div>
  `,
})
export class InputError {
  @Input({ required: true }) inputControl!: FormControl<any>;
  errorMessagesDictionary = input<{ [key: string]: string }>({});

  getErrorMessage() {
    let errors = this.inputControl.errors;
    let firstErrorKey = errors !== null ? Object.keys(errors)[0] : null;
    if (firstErrorKey === null) return 'This input is invalid';
    let firstErrorMessage = this.errorMessagesDictionary()[firstErrorKey];
    return firstErrorMessage ?? 'This input is invalid';
  }
}
