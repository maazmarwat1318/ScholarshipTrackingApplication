import { Component } from '@angular/core';
import { ActionDialogService } from './ActionDialogService';

@Component({
  selector: 'action-dialog',
  standalone: false,
  template: `
    <div
      class="modal fade bg-black"
      style="--bs-bg-opacity: 0.4"
      [attr.aria-labelledby]="actionDialogService.title()"
      [class.show]="actionDialogService.show()"
      [style.display]="actionDialogService.show() ? 'block' : 'none'"
    >
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" [id]="actionDialogService.title()">
              {{ actionDialogService.title() }}
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
              (click)="actionDialogService.hideDialog()"
              [disabled]="actionDialogService.isLoading()"
            ></button>
          </div>
          <div class="modal-body">
            {{ actionDialogService.message() }}
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
              (click)="actionDialogService.hideDialog()"
              [disabled]="actionDialogService.isLoading()"
            >
              Close
            </button>
            <button
              type="button"
              class="btn"
              loading-btn
              [isLoading]="actionDialogService.isLoading()"
              [class]="'btn-' + actionDialogService.confirmActionColor()"
              (click)="actionDialogService.triggerConfirmAction()"
              [disabled]="actionDialogService.isLoading()"
            >
              {{ actionDialogService.confirmActionLabel() }}
            </button>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class ActionDialogComponent {
  constructor(public actionDialogService: ActionDialogService) {}
}
