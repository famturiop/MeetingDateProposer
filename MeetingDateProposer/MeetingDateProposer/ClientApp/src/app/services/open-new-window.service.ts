import { Injectable } from '@angular/core';
import { IUser } from '../models/user.model';
import { AppConfigService } from '../app-config.service';

@Injectable()
export class OpenNewWindowService {

  private openWindowReference: (Window|null) = null;
  private settings = AppConfigService.settings.googleOAuthSettings;

  constructor(private window: Window) { }

  buildURL(user: IUser): string {
    const authEndpoint: string = this.settings.authEndpoint;
    const accessType: string = this.settings.accessType;
    const state: string = user.id;
    const responseType: string = this.settings.responseType;
    const clientId: string = this.settings.clientId;
    const redirectUri: string = this.settings.redirectUri;
    const scope: string = this.settings.scope;
    const flowName: string = this.settings.flowName;
    return `${authEndpoint}?access_type=${accessType}&response_type=${responseType}&state=${state}&client_id=${clientId}&redirect_uri=${redirectUri}&scope=${scope}&flowName=${flowName}`;
  }

  openNewWindow(url: string, name: string): void {
    const strWindowFeatures = 'toolbar=no, menubar=no, width=600, height=700, top=100, left=100';
    if (this.openWindowReference === null || this.openWindowReference.closed) {
      this.openWindowReference = this.window.open(url, name, strWindowFeatures);
    }
    else {
      this.openWindowReference.focus();
    }
  }
}
