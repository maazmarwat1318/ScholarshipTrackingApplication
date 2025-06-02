import { Component, computed, input, model } from '@angular/core';
import { SnackbarService } from './SnackbarService';
import { SnackbarType } from '../../enums';

@Component({
  selector: 'snackbar',
  standalone: false,
  template: `
    <div
      class="toast-container mt-3 top-0 start-50 position-fixed translate-middle-x"
    >
      <div
        id="toastError"
        class="toast"
        [class.show]="service.show()"
        role="alert"
        aria-live="assertive"
        aria-atomic="true"
      >
        <div
          class="toast-header bg-success"
          [class.bg-danger]="isErrorSnackbar()"
        >
          <strong class="me-auto text-white h6 mb-0">{{
            service.type()
          }}</strong>
          <button
            type="button"
            class="btn-close btn-close-white"
            (click)="service.hideSnackbar()"
          ></button>
        </div>
        <div
          class="toast-body mb-0 bg-success"
          [class.bg-danger]="isErrorSnackbar()"
          style="--bs-bg-opacity: 0.2"
        >
          {{ service.message() }}
        </div>
      </div>
    </div>
  `,
})
export class SnackbarComponent {
  isErrorSnackbar = computed(() => this.service.type() === SnackbarType.Error);
  constructor(public service: SnackbarService) {}
}
