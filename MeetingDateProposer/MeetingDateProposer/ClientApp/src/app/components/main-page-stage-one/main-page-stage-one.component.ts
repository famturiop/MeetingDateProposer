import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiMeetingService } from 'src/app/api-services/api-meeting.service';
import { MeetingService } from 'src/app/services/meeting.service';
import { IMeeting } from 'src/app/models/meeting.model';

@Component({
  selector: 'app-main-page-stage-one',
  templateUrl: './main-page-stage-one.component.html',
  styleUrls: ['./main-page-stage-one.component.scss']
})
export class MainPageStageOneComponent {

  public meeting: IMeeting = {id: "", connectedUsers: [], name: ""};
  public isDisabled: boolean = true;

  constructor( 
    private apiMeetingService: ApiMeetingService,
    private router: Router,
    private meetingService: MeetingService) {  
    }

  createMeeting(meetingName: string): void{
    this.meeting.name = meetingName;
    this.apiMeetingService.createMeeting(this.meeting).subscribe((response) => {
      this.meeting = response;
      this.meetingService.updateMeeting(this.meeting);
      this.router.navigate(['/stageTwo',this.meeting.id]);
    });
  }

  meetingTitleCheck(meetingName: string): void {
    if (meetingName.isEmpty()) {
      this.isDisabled = true;
    }
    else {
      this.isDisabled = false;
    }
  }
}
