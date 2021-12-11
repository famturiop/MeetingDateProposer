import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TopNavbarComponent } from './components/top-navbar/top-navbar.component';
import { MainPageStageOneComponent } from './components/main-page-stage-one/main-page-stage-one.component';
import { MainPageStageTwoComponent } from './components/main-page-stage-two/main-page-stage-two.component';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { BottomOutlineComponent } from './components/bottom-outline/bottom-outline.component';
import { MessagesComponent } from './components/messages/messages.component';
import { ExternalAuthorizationComponent } from './components/external-authorization/external-authorization.component';
import { AppConfigService } from './app-config.service';

export function initializeApp(appConfigService: AppConfigService) {
  return (): Promise<any> => { 
    return appConfigService.load();
  }
}

@NgModule({
  declarations: [
    AppComponent,
    TopNavbarComponent,
    MainPageStageOneComponent,
    MainPageStageTwoComponent,
    AboutPageComponent,
    BottomOutlineComponent,
    MessagesComponent,
    ExternalAuthorizationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: initializeApp, deps: [AppConfigService], multi: true},
    { provide: Window, useValue: window }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
