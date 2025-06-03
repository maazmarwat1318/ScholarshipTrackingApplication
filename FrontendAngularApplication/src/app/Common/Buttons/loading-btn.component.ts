import { Component, input, output } from '@angular/core';

@Component({
  selector: '[loading-btn]',
  template: `
    <span
      [class.visually-hidden]="!isLoading()"
      class="spinner-border spinner-border-sm mr-2"
      role="status"
      aria-hidden="true"
    ></span>
    <ng-content></ng-content>
  `,
  standalone: false,
})
export class LoadingBtnComponent {
  isLoading = input(false);
}
