import { Component } from '@angular/core';
import { ActionDialogService } from '../ActionDialog/ActionDialogService';
import { ContentDialogService } from './ContentDialogService';

@Component({
  selector: 'content-dialog',
  standalone: false,
  template: `
    <div
      class="modal fade p-3 bg-black"
      style="--bs-bg-opacity: 0.4"
      [class.show]="dialogService.visible()"
      [style.display]="dialogService.visible() ? 'block' : 'none'"
      role="dialog"
    >
      <div
        class="modal-dialog modal-dialog-centered modal-dialog-scrollable"
        style="width: 100%;"
        [style]="dialogService.styles()"
        role="document"
      >
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ dialogService.title() }}</h5>
            <button
              type="button"
              class="btn-close"
              aria-label="Close"
              [disabled]="this.dialogService.closeButtonDisabled()"
              (click)="this.dialogService.close()"
            ></button>
          </div>
          <div class="modal-body">
            <ng-container
              *ngTemplateOutlet="dialogService.content()"
            ></ng-container>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class ContentDialogComponent {
  constructor(public dialogService: ContentDialogService) {}
}
