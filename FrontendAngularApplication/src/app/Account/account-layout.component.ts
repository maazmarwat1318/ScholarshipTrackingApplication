import { Component, computed, Signal } from '@angular/core';
import { UserService } from '../Services/UserService';
import { AccountSectionMetaData } from './State/AccountSectionMetaData';

@Component({
  selector: 'account-component',
  templateUrl: './account.component.html',
  standalone: false,
  styleUrl: './account.component.css',
})
export class AccountLayoutComponent {
  sectionTitle: Signal<string>;
  sectionSubtitle: Signal<string>;
  constructor(
    private _userService: UserService,
    private _accountSectionMetaData: AccountSectionMetaData,
  ) {
    (this.sectionSubtitle = computed(() =>
      _accountSectionMetaData.sectionSubtitle(),
    )),
      (this.sectionTitle = computed(() =>
        _accountSectionMetaData.sectionTitle(),
      ));
  }
}
