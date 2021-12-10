import { User } from '../models/User';

import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { StageOneService } from '../stage-one.service';
import { MeetingService } from '../meeting.service';
import { Meeting } from '../models/Meeting';

@Component({
  selector: 'app-main-page-stage-one',
  templateUrl: './main-page-stage-one.component.html',
  styleUrls: ['./main-page-stage-one.component.css']
})
export class MainPageStageOneComponent implements OnInit {

  public meeting: Meeting = {id: "", connectedUsers: [], name: ""};

  constructor(private route: ActivatedRoute, 
    private location: Location, 
    private stageOneService: StageOneService,
    private router: Router,
    private meetingService: MeetingService) {  }

  ngOnInit(): void {
  }

  createMeeting(meetingName: string): void{
    this.meeting.name = meetingName;
    this.stageOneService.createMeeting(this.meeting).subscribe((response) => {
      this.meeting = response;
    },
    (error) => {

    },
    ()=>{
      this.meetingService.updateMeeting(this.meeting);
      this.router.navigate(['/stageTwo',this.meeting.id]);
    });
  }
}
