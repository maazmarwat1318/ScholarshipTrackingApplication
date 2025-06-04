import { Injectable, signal } from '@angular/core';

export type ConfirmActionColor = 'primary' | 'success' | 'danger';

@Injectable()
export class ActionDialogService {
  show = signal(false);
  title = signal('');
  message = signal('');
  confirmActionLabel = signal('Confirm');
  confirmActionColor = signal<ConfirmActionColor>('primary');
  confirmAction = signal<(() => void) | null>(null);
  isLoading = signal(false);

  showConfirmationDialog(
    title: string,
    message: string,
    confirmLabel: string = 'Confirm',
    confirmColor: ConfirmActionColor = 'primary',
    action: (() => void) | null = null,
  ) {
    this.title.set(title);
    this.message.set(message);
    this.confirmActionLabel.set(confirmLabel);
    this.confirmActionColor.set(confirmColor);
    this.confirmAction.set(action);
    this.isLoading.set(false); // Ensure not in loading state initially
    this.show.set(true);
  }

  setLoading(loading: boolean) {
    this.isLoading.set(loading);
  }

  hideDialog() {
    this.show.set(false);
    this.confirmAction.set(null); // Clear the action when hiding
  }

  triggerConfirmAction() {
    if (!this.isLoading() && this.confirmAction()) {
      this.confirmAction()!();
    }
  }
}
