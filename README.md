# ScholarshipTrackingApplication

## Make sure to create your appsettings.json files for WebAPI project and MVCPresentation Layer Project, you can check out what needs to be set by looking at the options used in Configuration Folder's Inject Options file

I Know there are better ways to do this but I am jsut Lazy, The appsetting sfile looks like this for both layers
Email Service used is mailtrap where you can go and create a demo domain with 20 free emails. Captcha is google recaptcha
Also the email service with demo domain can only send emails to the email the mailtrap account was created on which you will
have to hard code in the EmailService file in mehtod SendResetPasswordEmail -> variable emailData ->filed to

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DatabaseConnection": "Your MySQL Db Connection String"
  },
  "JwtOptions": {
    "Issuer": "STAIssuer",
    "Audience": "STAAudience",
    "ResetPasswordAudience": "ResetPasswordAudience",
    "SecretKey": "Your secret/private key for signing Tokens",
    "AccessTokenExpiryDays": 1,
    "ResetPasswordTokenExpiryMinutes": 15
  },
  "EmailServiceOptions": {
    "Host": "Your email service Host email. This porject uses mailtrap, where you can create a demo domain",
    "ApiToken": "your email api token for mailtrap",
    "ResetPasswordUrl": "http://localhost:4200/account/reset-password"
  },
  "CaptchaOptions": {
    "ClientKey": "Your Google Captcha Client Key",
    "ServerKey": "Your Google Captcha Server Key"
  }
}
```

## Also make sure to set environments for Fornt end application here is the object

Captcha has your google captcha script source and the key(ClientKey), APi Url is the Url Backend is running at

```javascript
export const environment = {
  captcha: {
    key: "",
    script: "",
  },
  apiUrl: "",
};
```
