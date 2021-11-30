import { Component, OnInit } from '@angular/core';
import { Meeting } from '../domain-objects/Meeting';
import { MeetingService } from '../meeting.service';
import { StageTwoService } from '../stage-two.service';
import { Router } from '@angular/router';
import { User } from '../domain-objects/User';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-main-page-stage-two',
  templateUrl: './main-page-stage-two.component.html',
  styleUrls: ['./main-page-stage-two.component.css']
})
export class MainPageStageTwoComponent implements OnInit {

  public meeting: Meeting = {id: "", connectedUsers: [], name: ""};
  public baseUrl = location.origin;
  public user: User = {calendars:[],credentials:null,id:"",userMeetings:[], name: ""};
  public currentRoute = this.routes.url;

  constructor(private meetingService: MeetingService,
    private stageTwoService: StageTwoService,
    private clipboard: Clipboard,
    private routes: Router) { }

  ngOnInit(): void {
    this.meetingService.currentMeeting.subscribe(meeting => {
      this.meeting = meeting;
    });
    if (this.meeting.id === "") {
      this.meeting.id = this.currentRoute.split("/")[2];
      this.stageTwoService.getMeeting(this.meeting).subscribe((response) => {
        this.meeting = response;
      },
      (error) => {
  
      },
      ()=>{

      });
      this.meetingService.updateMeeting(this.meeting);
    }
  }

  createUser(userName: string): void {
    this.user.name = userName;
    this.stageTwoService.createUser(this.user).subscribe((response) => {
      this.user = response;
    },
    (error) => {

    },
    ()=>{

    });
  }

  addUserToMeeting(): void {
    this.stageTwoService.updateMeeting(this.user,this.meeting).subscribe((response) => {
      this.meeting = response;
    },
    (error) => {

    },
    ()=>{

    });
  }

  displayLink(): string {
    return this.baseUrl+this.currentRoute;
  }

  copyLink(): void {
    this.clipboard.copy(this.baseUrl+this.currentRoute);
  }

  ngOnChanges() {

  }

  ngOnDestroy() {

  }

}
