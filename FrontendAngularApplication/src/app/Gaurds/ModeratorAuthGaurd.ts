import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  GuardResult,
  MaybeAsync,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { UserService } from '../Services/UserService';

@Injectable({ providedIn: 'root' })
export class ModeratorAuthGaurd implements CanActivate {
  constructor(
    private _userService: UserService,
    private router: Router,
  ) {}
  canActivate(
    _: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): MaybeAsync<GuardResult> {
    if (this._userService.isModerator()) {
      return true;
    } else {
      this.router.navigate(['/account/login'], {
        queryParams: { returnUrl: state.url },
      });
      return false;
    }
  }
}
