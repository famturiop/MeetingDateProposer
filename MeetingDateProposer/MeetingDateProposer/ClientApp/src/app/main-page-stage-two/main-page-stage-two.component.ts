import { Component, HostListener, OnInit } from '@angular/core';
import { Meeting } from '../models/Meeting';
import { MeetingService } from '../meeting.service';
import { StageTwoService } from '../stage-two.service';
import { Router } from '@angular/router';
import { User } from '../models/User';
import { Clipboard } from '@angular/cdk/clipboard';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-main-page-stage-two',
  templateUrl: './main-page-stage-two.component.html',
  styleUrls: ['./main-page-stage-two.component.css']
})
export class MainPageStageTwoComponent implements OnInit {

  public meeting: Meeting = {id: "", connectedUsers: [], name: ""};
  private openWindowReference: (Window|null) = null;

  constructor(private meetingService: MeetingService,
    private stageTwoService: StageTwoService,
    private clipboard: Clipboard,
    private routes: Router,
    private window: Window) { }

  ngOnInit(): void {
    this.meetingService.currentMeeting.subscribe(meeting => {
      this.meeting = meeting;
    });
    if (this.meeting.id === "") {
      this.meeting.id = this.routes.url.split("/")[2];
      this.stageTwoService.getMeeting(this.meeting).subscribe((response) => {
        this.meetingService.updateMeeting(response);
      },
      (error) => {
  
      },
      ()=>{

      });
    }
  }

  private createUser(userName: string): Observable<User> {
    let user: User = {calendars:[],credentials:null,id:"", name: userName};
    return this.stageTwoService.createUser(user);
  }

  addUserToMeeting(userName: string): void {
    this.createUser(userName).pipe(switchMap(user => {
      return this.stageTwoService.updateMeeting(user,this.meeting);
    })).subscribe((response) => {
      this.meetingService.updateMeeting(response);
    },
    (error) => {

    },
    ()=>{

    });
  }

  addCalendarToUser(user: User): void {
    const strWindowFeatures = 'toolbar=no, menubar=no, width=600, height=700, top=100, left=100';
    const url = this.buildURL(user);
    const name = "";
    if (this.openWindowReference === null || this.openWindowReference.closed) {
      this.openWindowReference = this.window.open(url, name, strWindowFeatures);
    }
    else {
      this.openWindowReference.focus();
    }
    
  }

  @HostListener('window:message',['$event']) recieveAuthCode(event: MessageEvent<string[]>): void {
    let code = event.data[0];
    let userId = event.data[1];
    let meeting = this.meeting;
    this.meeting.connectedUsers.forEach((user,userIndex) => {
      if (user.id === userId){
        this.stageTwoService.updateUser(user,code as string).subscribe((response) => {
          meeting.connectedUsers[userIndex] = response;
          this.meetingService.updateMeeting(meeting);
        },
        (error) => {
          
        },
        ()=>{
    
        });
      }
    })
  }

  buildURL(user: User): string {
    const authEndpoint: string = 'https://accounts.google.com/o/oauth2/v2/auth';
    const accessType: string = 'offline';
    const state: string = user.id;
    const responseType: string = 'code';
    const clientId: string = '210750196305-c4dqfmn8emrlmbb0s0a38uuihhrp5a6m.apps.googleusercontent.com';
    const redirectUri: string = 'http%3A%2F%2Flocalhost%3A4200%2Fauthorize%2F';
    const scope: string = 'https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fcalendar.readonly';
    const flowName: string = 'GeneralOAuthFlow';
    return `${authEndpoint}?access_type=${accessType}&response_type=${responseType}&state=${state}&client_id=${clientId}&redirect_uri=${redirectUri}&scope=${scope}&flowName=${flowName}`;
  }

  displayLink(): string {
    return this.window.location.origin+this.routes.url;
  }

  copyLink(): void {
    this.clipboard.copy(this.window.location.origin+this.routes.url);
  }

  ngOnChanges() {
    this.meetingService.currentMeeting.subscribe(meeting => {
      this.meeting = meeting;
    });
  }

  ngOnDestroy() {

  }

}
