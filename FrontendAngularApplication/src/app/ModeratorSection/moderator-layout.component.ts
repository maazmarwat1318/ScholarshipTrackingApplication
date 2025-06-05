import { Component, computed, Signal } from '@angular/core';
import { UserService } from '../Services/UserService';
import { Title } from '@angular/platform-browser';
import { TitleService } from './Service/TitleService';
import { Router } from '@angular/router';

@Component({
  selector: 'moderator-component',
  templateUrl: './moderator.component.html',
  standalone: false,
})
export class ModeratorLayoutComponent {
  readonly title = computed<string>(() => this._titleService.title());
  constructor(
    public _titleService: TitleService,
    public _userService: UserService,
    public _router: Router,
  ) {}
  onLogout() {
    this._userService.onLogout();
    this._router.navigateByUrl('/account/login');
  }
}
