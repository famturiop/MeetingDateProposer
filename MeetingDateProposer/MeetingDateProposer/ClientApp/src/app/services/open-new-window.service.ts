import { Injectable } from '@angular/core';

@Injectable()
export class OpenNewWindowService {

  private openWindowReference: (Window|null) = null;

  constructor(private window: Window) { }

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
