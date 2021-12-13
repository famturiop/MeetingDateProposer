import { Component, HostListener, OnInit } from '@angular/core';
import { IMeeting } from 'src/app/models/meeting.model';
import { MeetingService } from 'src/app/services/meeting.service';
import { ApiMeetingService } from 'src/app/api-services/api-meeting.service';
import { ApiUserService } from 'src/app/api-services/api-user.service';
import { Router } from '@angular/router';
import { IUser } from 'src/app/models/user.model';
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

  public meeting: IMeeting = {id: "", connectedUsers: [], name: ""};

  constructor(private meetingService: MeetingService,
    private apiMeetingService: ApiMeetingService,
    private apiUserService: ApiUserService,
    private clipboard: Clipboard,
    private router: Router,
    private window: Window,
    private newWindow: OpenNewWindowService) { }

  ngOnInit(): void {
    this.meetingService.currentMeeting.subscribe(meeting => {
      this.meeting = meeting;
    });
    if (this.meeting.id === "") {
      this.meeting.id = this.router.url.split("/")[2];
      this.apiMeetingService.getMeeting(this.meeting).subscribe((response) => {
        this.meetingService.updateMeeting(response);
      },
      (error) => {
  
      },
      ()=>{

      });
    }
  }

  private createUser(userName: string): Observable<IUser> {
    let user: IUser = {calendars:[],credentials:null,id:"", name: userName};
    return this.apiUserService.createUser(user);
  }

  addUserToMeeting(userName: string): void {
    this.createUser(userName).pipe(switchMap(user => {
      return this.apiMeetingService.updateMeeting(user,this.meeting);
    })).subscribe((response) => {
      this.meetingService.updateMeeting(response);
    },
    (error) => {

    },
    ()=>{

    });
  }

  addCalendarToUser(user: IUser): void {
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
        this.apiUserService.updateUser(user,code as string).subscribe((response) => {
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
    return this.window.location.origin+this.router.url;
  }

  copyLink(): void {
    this.clipboard.copy(this.window.location.origin+this.router.url);
  }

  ngOnChanges() {
    this.meetingService.currentMeeting.subscribe(meeting => {
      this.meeting = meeting;
    });
  }

  ngOnDestroy() {

  }

}
