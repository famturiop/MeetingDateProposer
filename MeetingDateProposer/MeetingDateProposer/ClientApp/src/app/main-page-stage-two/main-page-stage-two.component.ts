import { Component, OnInit } from '@angular/core';
import { Meeting } from '../domain-objects/Meeting';
import { MeetingService } from '../meeting.service';
import { StageTwoService } from '../stage-two.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-main-page-stage-two',
  templateUrl: './main-page-stage-two.component.html',
  styleUrls: ['./main-page-stage-two.component.css']
})
export class MainPageStageTwoComponent implements OnInit {

  constructor(private meetingService: MeetingService,
    private stageTwoService: StageTwoService,
    private locationService: Location) { }
  public meeting: Meeting = {id: "123", connectedUsers: [], name: "hai"};
  public baseUrl = location.origin;
  public meeting1: string = this.meeting.name;

  ngOnInit(): void {
    this.meetingService.currentMeeting.subscribe(meeting => {
      this.meeting = meeting;
    });
  }

  displayLink(): string {
    const trimmedUrl = "/stageTwo/";
    if (this.meeting.id === "") {
      this.meeting.id = this.locationService.path().substr(trimmedUrl.length);
      this.stageTwoService.getMeeting(this.meeting).subscribe((response) => {
        this.meeting = response;
      },
      (error) => {
  
      },
      ()=>{

      });
    }
    return this.baseUrl+trimmedUrl+this.meeting.id;
  }

}
