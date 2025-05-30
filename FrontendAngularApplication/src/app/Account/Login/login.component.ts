import { ChangeDetectionStrategy, Component, OnInit, signal } from "@angular/core";
import { AccountSectionMetaData } from "../State/AccountSectionMetaData";
import { Title } from "@angular/platform-browser";
import { FormControl, FormGroup, FormSubmittedEvent } from "@angular/forms";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    standalone: false,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent{
    public loginForm = new FormGroup({
        email: new FormControl<string>(""),
        password: new FormControl<string>("")
    });
    isFormLoading = signal(false);

    
    constructor(private _accountSectionMetaData: AccountSectionMetaData, private _pageTitle: Title, private _httpClient: HttpClient){
        _accountSectionMetaData.sectionSubtitle.set("Enter your email and password to login");
        _accountSectionMetaData.sectionTitle.set("Login");
        _pageTitle.setTitle("Login")
    }

    onSubmit = async (event: Event) => {
        event.preventDefault()
        this.loginForm.markAllAsTouched()
        if(this.loginForm.valid) {
            this.isFormLoading.set(true)
            this._httpClient.post("https://localhost:7222/Account/Login", this.loginForm.value).subscribe(response => {
                console.log(response);
            this.isFormLoading.set(false);
        }, error => {
            console.log(error)
            this.isFormLoading.set(false);
        });
    }
    }
}