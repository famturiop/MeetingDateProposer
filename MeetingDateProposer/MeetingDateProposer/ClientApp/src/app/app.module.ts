import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TopNavbarComponent } from './top-navbar/top-navbar.component';
import { MainPageStageOneComponent } from './main-page-stage-one/main-page-stage-one.component';
import { MainPageStageTwoComponent } from './main-page-stage-two/main-page-stage-two.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { BottomOutlineComponent } from './bottom-outline/bottom-outline.component';
import { MessagesComponent } from './messages/messages.component';
import { ExternalAuthorizationComponent } from './external-authorization/external-authorization.component';

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
    { provide: Window, useValue: window }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
