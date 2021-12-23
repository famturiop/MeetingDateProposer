import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ApiMeetingService } from 'src/app/api-services/api-meeting.service';
import { MeetingService } from 'src/app/services/meeting.service';
import { IMeeting } from 'src/app/models/meeting.model';

@Component({
  selector: 'app-main-page-stage-one',
  templateUrl: './main-page-stage-one.component.html',
  styleUrls: ['./main-page-stage-one.component.css']
})
export class MainPageStageOneComponent implements OnInit {

  public meeting: IMeeting = {id: "00000000-0000-0000-0000-000000000000", connectedUsers: [], name: ""};

  constructor(private route: ActivatedRoute, 
    private location: Location, 
    private apiMeetingService: ApiMeetingService,
    private router: Router,
    private meetingService: MeetingService) {  }

  ngOnInit(): void {
  }

  createMeeting(meetingName: string): void{
    this.meeting.name = meetingName;
    this.apiMeetingService.createMeeting(this.meeting).subscribe((response) => {
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
