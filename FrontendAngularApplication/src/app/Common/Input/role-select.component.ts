import { Component, input, OnInit, signal } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Role } from '../enums';

@Component({
  selector: 'role-select',
  standalone: false,
  template: `
    <div>
      <label [for]="selectFor()" class="form-label">
        {{ selectLabel() }}
      </label>
      <select
        [id]="selectFor()"
        [formControl]="roleControl()"
        class="form-select"
        [class.is-invalid]="!roleControl().valid && roleControl().touched"
      >
        <option [value]="null">{{ selectPlaceholder() }}</option>
        <option *ngFor="let role of roles()" [value]="role">
          {{ role }}
        </option>
      </select>
      <input-error
        [errorMessagesDictionary]="errorMessages"
        [inputControl]="roleControl()"
      />
    </div>
  `,
})
export class RoleSelectComponent implements OnInit {
  roleControl = input.required<FormControl<Role | null>>();
  selectFor = input('role');
  selectLabel = input('Role');
  selectPlaceholder = input('-- Select a Role --');
  required = input(true);

  // Directly use the enum values
  roles = signal<Role[]>(
    Object.values(Role).filter((role) => role !== Role.Student),
  );

  constructor() {}

  errorMessages = {
    required: `${this.selectLabel()} is required`,
  };

  ngOnInit(): void {
    const ctrl = this.roleControl();
    if (this.required()) {
      ctrl.setValidators([Validators.required]);
    }
    ctrl.updateValueAndValidity();
  }
}
