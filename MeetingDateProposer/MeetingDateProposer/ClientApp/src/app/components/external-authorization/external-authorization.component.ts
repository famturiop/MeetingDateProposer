import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-external-authorization',
  templateUrl: './external-authorization.component.html',
  styleUrls: ['./external-authorization.component.css']
})
export class ExternalAuthorizationComponent implements OnInit {

  constructor(private activeRoute: ActivatedRoute, private window: Window) { }

  ngOnInit(): void {
    this.activeRoute.queryParamMap.subscribe(params => {
      if (params.get('code') !== null && params.get('state') !== null){
        let code: string = params.get('code') as string;
        let state: string = params.get('state') as string;
        let message: (string)[] = [code,state];
        if (window.opener) {
          this.window.opener.postMessage(message,this.window.location.origin);
          this.window.close();
        }
      }
      else {
        throw new Error("One or more of the authentication parameters are null.");
      }
    });
  }

}
