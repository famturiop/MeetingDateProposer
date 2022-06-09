import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageStageOneComponent } from './components/main-page-stage-one/main-page-stage-one.component';
import { MainPageStageTwoComponent } from './components/main-page-stage-two/main-page-stage-two.component';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { ExternalAuthorizationComponent } from './components/external-authorization/external-authorization.component';
import { ErrorComponent } from './components/error/error.component';
import { TermsComponent } from './components/terms/terms.component';
import { PrivacyComponent } from './components/privacy/privacy.component';
import { HomeComponent } from './components/home/home.component';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'stageTwo/:id', component: MainPageStageTwoComponent},
  {path: 'about', component: AboutPageComponent},
  {path: 'stageOne', component: MainPageStageOneComponent},
  {path: 'authorize', component: ExternalAuthorizationComponent},
  {path: 'terms', component: TermsComponent},
  {path: 'privacy', component: PrivacyComponent},
  {path: '**', component: ErrorComponent}
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
