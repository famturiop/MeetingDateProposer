import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-top-navbar',
  templateUrl: './top-navbar.component.html',
  styleUrls: ['./top-navbar.component.scss']
})
export class TopNavbarComponent implements OnInit {

  public readonly imageUrl: string = "/assets/icon.svg";
  
  constructor() { }

  ngOnInit(): void {

  }
}
