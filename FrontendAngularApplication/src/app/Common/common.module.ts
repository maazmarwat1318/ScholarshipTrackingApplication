import { NgModule } from "@angular/core";
import { NameInputComponent } from "./Input/name-input.component";
import { ReactiveFormsModule } from "@angular/forms";
import { PasswordInputComponent } from "./Input/password-input.component";
import { EmailInputComponent } from "./Input/email-input.component";
import { NgIf } from "@angular/common";
import { InputError } from "./Input/input-error.component";

@NgModule({
    declarations: [NameInputComponent, PasswordInputComponent, EmailInputComponent, InputError],
    exports: [NameInputComponent, ReactiveFormsModule, PasswordInputComponent, EmailInputComponent, NgIf],
    imports: [ReactiveFormsModule, NgIf]
})
export class CommonModule{} 