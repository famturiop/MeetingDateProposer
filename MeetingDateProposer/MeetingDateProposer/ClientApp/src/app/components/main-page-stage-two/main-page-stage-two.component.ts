import { Component, HostListener, OnInit } from '@angular/core';
import { Meeting } from 'src/app/models/Meeting';
import { MeetingService } from 'src/app/services/meeting.service';
import { StageTwoService } from 'src/app/api-services/stage-two.service';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { Clipboard } from '@angular/cdk/clipboard';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { OpenNewWindowService } from 'src/app/services/open-new-window.service';

@Component({
  selector: 'app-main-page-stage-two',
  templateUrl: './main-page-stage-two.component.html',
  styleUrls: ['./main-page-stage-two.component.css']
})
export class MainPageStageTwoComponent implements OnInit {

  public meeting: Meeting = {id: "", connectedUsers: [], name: ""};

  constructor(private meetingService: MeetingService,
    private stageTwoService: StageTwoService,
    private clipboard: Clipboard,
    private routes: Router,
    private window: Window,
    private newWindow: OpenNewWindowService) { }

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
    const url = this.newWindow.buildURL(user);
    const name = "";
    this.newWindow.openNewWindow(url,name);
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
