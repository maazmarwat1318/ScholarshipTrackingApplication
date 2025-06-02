import { Component, input, output } from '@angular/core';

@Component({
  selector: 'primary-btn',
  template: ` <button
    (click)="onClicked($event)"
    [disabled]="isLoading()"
    id="form-btn"
    [type]="btnType()"
    class="btn btn-primary w-100"
  >
    <span
      [class.visually-hidden]="!isLoading()"
      class="spinner-border spinner-border-sm mr-2"
      role="status"
      aria-hidden="true"
    ></span>
    <ng-content></ng-content>
  </button>`,
  standalone: false,
})
export class PrimaryBtnComponent {
  isLoading = input(false);
  btnType = input<string>();
  onClick = output<MouseEvent>();

  onClicked(e: MouseEvent) {
    this.onClick.emit(e);
  }
}
