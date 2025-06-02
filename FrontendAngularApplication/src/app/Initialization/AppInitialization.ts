import { Injectable } from '@angular/core';
import { UserService } from '../Services/UserService';

@Injectable({ providedIn: 'root' })
export class AppInitialization {
  constructor(private _userService: UserService) {}

  onInit = async () => {
    await this.loadUserCookie();
  };

  private loadUserCookie = async () => {
    await this._userService.onLoadAuthCookie();
  };
}
