import { Injectable, signal, WritableSignal } from "@angular/core";
import { CookieService } from "ngx-cookie-service";

@Injectable({providedIn: 'root'})
export class UserService {
    constructor(private _cookieService: CookieService) {}

    authToken: string | null = null;

    onLoadAuthCookie = async () => {
        let cookie = this._cookieService.get("authToken");
        if(!cookie) return;
        this.authToken = cookie;
    }

    onDeleteAuthCookie = async () => {
        this._cookieService.delete("authToken");
    }

    isAuthenticated = () => {
        return this.authToken !== null;
    }
}