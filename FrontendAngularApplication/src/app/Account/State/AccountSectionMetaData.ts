import { Injectable, signal, Signal } from '@angular/core';
import { AccountModule } from '../account.module';

@Injectable({ providedIn: AccountModule })
export class AccountSectionMetaData {
  sectionTitle = signal('');
  sectionSubtitle = signal('');
  constructor() {}
}
