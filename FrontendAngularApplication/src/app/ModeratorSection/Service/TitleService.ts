import { Injectable, signal } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable()
export class TitleService {
  private _title = signal('Scholarship Tracking Application');
  public readonly title = this._title.asReadonly();
  constructor(private _pageTitle: Title) {}
  setTitle(value: string) {
    this._title.set(value);
    this._pageTitle.setTitle(value);
  }
}
