import { inject, NgModule, provideAppInitializer } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CookieService } from 'ngx-cookie-service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppInitialization } from './Initialization/AppInitialization';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from './Common/common.module';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { ScriptLoaderService } from './Services/ScriptLoader';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [
    CookieService,
    ScriptLoaderService,
    provideHttpClient(withFetch()),
    provideAppInitializer(() => {
      const initializationService = inject(AppInitialization);
      return initializationService.onInit();
    }),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
