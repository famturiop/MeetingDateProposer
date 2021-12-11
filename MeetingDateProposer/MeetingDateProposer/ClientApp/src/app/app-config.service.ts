import { Injectable } from '@angular/core';
import { IAppConfig } from './app-config.interface';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  static settings: IAppConfig;

  constructor(private http: HttpClient) { }
  load(){
    const jsonFile = `assets/app.config.json`;

    return new Promise<void>((resolve, reject) => {
      this.http.get<IAppConfig>(jsonFile)
      .toPromise<IAppConfig>().then((response : IAppConfig) => {
        AppConfigService.settings = <IAppConfig>response;

         console.log('Config Loaded');
         console.log( AppConfigService.settings);
         resolve();
         
      }).catch((response: any) => {
         reject(`Could not load the config file`);
      });
    });
  }
}
