import { Injectable, signal, WritableSignal } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Cookies, Role } from '../Common/enums';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private _cookieService: CookieService) {}

  authToken: string | null = null;
  userName: string | null = null;
  userId: number | null = null;
  userEmail: string | null = null;
  userRole: Role | null = null;

  onLoadAuthCookie = async () => {
    let cookie = this._cookieService.get(Cookies.AuthToken);
    if (!cookie) return;
    this.authToken = cookie;
    const { email, name, nameIdentifier, role } = this.extractClaims(cookie);
    this.userEmail = email;
    this.userName = name;
    this.userId = nameIdentifier;
    this.userRole = role;
  };

  onDeleteAuthCookie = async () => {
    this._cookieService.delete(Cookies.AuthToken);
    this.userEmail = null;
    this.userName = null;
    this.userId = null;
    this.userRole = null;
    this.authToken = null;
  };

  onLogout = async () => {
    this.onDeleteAuthCookie();
  };

  onSetAuthCookie = (cookie: string) => {
    this._cookieService.set(
      Cookies.AuthToken,
      cookie,
      1,
      undefined,
      undefined,
      true,
      'Strict',
    );
    const { email, name, nameIdentifier, role } = this.extractClaims(cookie);
    this.userEmail = email;
    this.userName = name;
    this.userId = nameIdentifier;
    this.userRole = role;
    this.authToken = cookie;
  };

  isAuthenticated = () => {
    return this.authToken !== null;
  };

  isModerator = () => {
    return (
      this.userRole === Role.Moderator || this.userRole === Role.SuperModerator
    );
  };

  isStudent = () => {
    return this.userRole === Role.Student;
  };

  isSuperModerator = () => {
    return this.userRole === Role.SuperModerator;
  };

  private decodeJwt(token: string) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join(''),
    );

    return JSON.parse(jsonPayload);
  }

  private extractClaims(token: string) {
    var claims = this.decodeJwt(token);
    var nameIdentifier = Number(
      claims[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ] as string,
    );
    var name = claims[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
    ] as string;
    var email = claims[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
    ] as string;
    var role = claims[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ] as Role;

    return {
      nameIdentifier,
      name,
      email,
      role,
    };
  }
}
