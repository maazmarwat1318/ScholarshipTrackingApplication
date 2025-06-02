import { Component, computed, input, OnInit, signal } from '@angular/core';
import { FormControl, FormControlStatus, Validators } from '@angular/forms';

@Component({
  selector: 'password-input',
  templateUrl: './input.component.html',
  standalone: false,
})
export class PasswordInputComponent implements OnInit {
  inputControl = input.required<FormControl<string | null>>();
  inputFor = input('Password');
  inputLabel = input('Password');
  inputPlaceholder = input('');
  inputType = 'password';
  errorMessages = {
    required: `${this.inputLabel()} is required`,
    minlength: `${this.inputLabel()} must have atleast 8 characters`,
  };

  ngOnInit(): void {
    const ctrl = this.inputControl();
    ctrl.setValidators([Validators.required, Validators.minLength(8)]);
    ctrl.updateValueAndValidity();
  }
}
