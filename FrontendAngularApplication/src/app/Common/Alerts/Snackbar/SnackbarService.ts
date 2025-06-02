import { Injectable, signal } from '@angular/core';
import { SnackbarType } from '../../enums';

@Injectable()
export class SnackbarService {
  show = signal(false);
  type = signal<SnackbarType>(SnackbarType.Success);
  message = signal('');

  autoHideTimeout: number | undefined = undefined;

  showSuccessSnackbar(message: string) {
    this.message.set(message);
    this.type.set(SnackbarType.Success);
    this.showSnackbar();
  }

  showErrorSnackbar(message: string) {
    this.message.set(message);
    this.type.set(SnackbarType.Error);
    this.showSnackbar();
  }

  hideSnackbar() {
    this.show.set(false);
  }

  private showSnackbar() {
    this.show.set(true);
    if (this.autoHideTimeout) {
      clearTimeout(this.autoHideTimeout);
    }
    this.autoHideTimeout = setTimeout(
      () => this.show.set(false),
      5000,
    ) as unknown as number;
  }
}
