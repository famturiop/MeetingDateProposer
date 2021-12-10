import { Injectable } from '@angular/core';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class OpenNewWindowService {

  private openWindowReference: (Window|null) = null;

  constructor(private window: Window) { }

  buildURL(user: User): string {
    const authEndpoint: string = 'https://accounts.google.com/o/oauth2/v2/auth';
    const accessType: string = 'offline';
    const state: string = user.id;
    const responseType: string = 'code';
    const clientId: string = '210750196305-c4dqfmn8emrlmbb0s0a38uuihhrp5a6m.apps.googleusercontent.com';
    const redirectUri: string = 'http%3A%2F%2Flocalhost%3A4200%2Fauthorize%2F';
    const scope: string = 'https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fcalendar.readonly';
    const flowName: string = 'GeneralOAuthFlow';
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
