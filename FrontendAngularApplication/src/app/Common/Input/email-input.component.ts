import {
  Component,
  computed,
  DoCheck,
  effect,
  input,
  OnChanges,
  OnInit,
  signal,
  SimpleChanges,
} from '@angular/core';
import { FormControl, FormControlStatus, Validators } from '@angular/forms';

@Component({
  selector: 'email-input',
  templateUrl: './input.component.html',
  standalone: false,
})
export class EmailInputComponent implements OnInit {
  inputControl = input.required<FormControl<string | null>>();
  inputFor = input('Email');
  inputLabel = input('Email');
  inputPlaceholder = input('');
  inputType = 'email';
  errorMessages = {
    required: `${this.inputLabel()} is required`,
    email: `Enter a valid ${this.inputLabel()}`,
  };
  ngOnInit(): void {
    const ctrl = this.inputControl();
    ctrl.setValidators([Validators.required, Validators.email]);
    ctrl.updateValueAndValidity();
  }
}
