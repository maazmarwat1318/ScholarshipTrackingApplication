import { Injectable, signal, TemplateRef } from '@angular/core';

@Injectable()
export class ContentDialogService {
  visible = signal(false);
  title = signal('');
  closeButtonDisabled = signal(false);
  content = signal<TemplateRef<any> | null>(null);
  styles = signal('');

  open(
    title: string,
    content: TemplateRef<any>,
    closeButtonDisabled: boolean = false,
    styles: string = '',
  ) {
    this.title.set(title);
    this.content.set(content);
    this.closeButtonDisabled.set(closeButtonDisabled);
    this.visible.set(true);
    this.styles.set(styles);
  }

  close() {
    this.visible.set(false);
    this.content.set(null);
  }
}
