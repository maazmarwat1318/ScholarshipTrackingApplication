import { Component, computed, input, OnInit, signal } from '@angular/core';
import { FormControl, FormControlStatus, Validators } from '@angular/forms';

@Component({
  selector: 'name-input',
  templateUrl: './input.component.html',
  standalone: false,
})
export class NameInputComponent implements OnInit {
  inputControl = input.required<FormControl<string | null>>();
  inputFor = input('name');
  inputLabel = input('Name');
  inputPlaceholder = input('');
  inputType = 'text';
  errorMessages = {
    required: `${this.inputLabel()} is required`,
  };

  ngOnInit(): void {
    const ctrl = this.inputControl();
    ctrl.setValidators([Validators.required]);
    ctrl.updateValueAndValidity();
  }
}
