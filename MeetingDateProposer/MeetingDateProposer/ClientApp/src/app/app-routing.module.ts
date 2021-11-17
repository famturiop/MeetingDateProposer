import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TopToolbarComponent } from './top-toolbar/top-toolbar.component';
import { MainPageStageOneComponent } from './main-page-stage-one/main-page-stage-one.component';
import { MainPageStageTwoComponent } from './main-page-stage-two/main-page-stage-two.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { BottomToolbarComponent } from './bottom-toolbar/bottom-toolbar.component';


const routes: Routes = [
  {path: '', redirectTo: 'stageOne', pathMatch: 'full'},
  {path: 'stageTwo', component: MainPageStageTwoComponent},
  {path: 'about', component: AboutPageComponent},
  {path: 'stageOne', component: MainPageStageOneComponent}

]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
