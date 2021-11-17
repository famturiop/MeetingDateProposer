import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { TopToolbarComponent } from './top-toolbar/top-toolbar.component';
import { MainPageStageOneComponent } from './main-page-stage-one/main-page-stage-one.component';
import { MainPageStageTwoComponent } from './main-page-stage-two/main-page-stage-two.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { BottomToolbarComponent } from './bottom-toolbar/bottom-toolbar.component';
import { MessagesComponent } from './messages/messages.component';

@NgModule({
  declarations: [
    AppComponent,
    TopToolbarComponent,
    MainPageStageOneComponent,
    MainPageStageTwoComponent,
    AboutPageComponent,
    BottomToolbarComponent,
    MessagesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
