import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-external-authorization',
  templateUrl: './external-authorization.component.html',
  styleUrls: ['./external-authorization.component.scss']
})
export class ExternalAuthorizationComponent implements OnInit {

  constructor(private activeRoute: ActivatedRoute, private window: Window) { 
  }

  ngOnInit(): void {
    this.activeRoute.queryParamMap.subscribe(params => {
      let code: string = params.get('code') || "";
      let state: string = params.get('state') || "";
      let error: string = params.get('error') || "";
      if (error === "access_denied"){
        this.closeCurrentWindow();
      }
      if (!code.isEmpty() && !state.isEmpty()){
        let message: (string)[] = [code,state];
        if (window.opener) {
          this.window.opener.postMessage(message,this.window.location.origin);
          this.closeCurrentWindow();
        }
      }
      else {
        throw new Error("One or more of the authentication parameters are null.");
      }
    });
  }

  private closeCurrentWindow(): void {
    this.window.onload = (ev: Event) => {
      this.window.close();
    };
  }
  
}
