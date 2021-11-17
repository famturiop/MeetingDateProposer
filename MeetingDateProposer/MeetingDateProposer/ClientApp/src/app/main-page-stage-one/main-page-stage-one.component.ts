import { User } from '../domain-objects/User';

import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { StageOneService } from '../stage-one.service';

@Component({
  selector: 'app-main-page-stage-one',
  templateUrl: './main-page-stage-one.component.html',
  styleUrls: ['./main-page-stage-one.component.css']
})
export class MainPageStageOneComponent implements OnInit {

  constructor(private route: ActivatedRoute, 
    private location: Location, 
    private stageOneService: StageOneService) {  }


  ngOnInit(): void {
  }

  public user: User = {calendars:[],credentials:null,id:0,userMeetings:[]};

  getCalendar(): void{
    this.stageOneService.getCalendar().subscribe(user => this.user={id: (user as any).id, calendars: (user as any).calendars});
  }

  goToStageTwo(): void {
    this.location.go("/stageTwo");
  }

}
