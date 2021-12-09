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
      let code: string|null  = params.get('code');
      let state: string|null = params.get('state');
      let message: (string|null)[] = [code,state];
      if (window.opener) {
        this.window.opener.postMessage(message,this.window.location.origin);
        this.window.close();
      }
    });
  }

}
