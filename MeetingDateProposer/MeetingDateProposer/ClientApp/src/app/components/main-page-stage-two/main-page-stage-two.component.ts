import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { IMeeting } from 'src/app/models/meeting.model';
import { MeetingService } from 'src/app/services/meeting.service';
import { ApiMeetingService } from 'src/app/api-services/api-meeting.service';
import { ApiUserService } from 'src/app/api-services/api-user.service';
import { Router } from '@angular/router';
import { IUser } from 'src/app/models/user.model';
import { Clipboard } from '@angular/cdk/clipboard';
import { Observable, Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-main-page-stage-two',
  templateUrl: './main-page-stage-two.component.html',
  styleUrls: ['./main-page-stage-two.component.scss']
})
export class MainPageStageTwoComponent implements OnInit, OnDestroy {

  public meeting: IMeeting = {id: "00000000-0000-0000-0000-000000000000", connectedUsers: [], name: ""};
  private meetingSubscription: Subscription;
  public isDisabled: boolean = true;
  public addCalendarIsVisible: boolean = false;
  public readonly iconUrl: string = "/assets/Google_Calendar_icon_(2020).svg";

  constructor(private meetingService: MeetingService,
    private apiMeetingService: ApiMeetingService,
    private apiUserService: ApiUserService,
    private clipboard: Clipboard,
    private router: Router,
    private window: Window) {
      this.meetingSubscription = this.meetingService.currentMeeting.subscribe(meeting => {
        this.meeting = meeting;
      });
     }

  ngOnInit(): void {
    if (this.meeting.id === "00000000-0000-0000-0000-000000000000") {
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
    let user: IUser = {calendars:[], id:"00000000-0000-0000-0000-000000000000", name: userName};
    return this.apiUserService.createUser(user);
  }

  addUserToMeeting(userName: string): void {
    this.createUser(userName).pipe(switchMap(user => {
      return this.apiMeetingService.addUserToMeeting(user,this.meeting);
    })).subscribe((response) => {
      this.meetingService.updateMeeting(response);
    },
    (error) => {

    },
    ()=>{

    });
  }

  @HostListener('window:message',['$event']) recieveAuthCode(event: MessageEvent<string[]>): void {
    let code = event.data[0];
    let userId = event.data[1];
    let meeting = Object.assign({},this.meeting);
    this.meeting.connectedUsers.forEach((user,userIndex) => {
      if (user.id === userId){
        this.apiUserService.addGoogleCalendarToUser(user,code as string).subscribe((response) => {
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

  userNameCheck(userName: string): void {
    if (userName.isEmpty()) {
      this.isDisabled = true;
    }
    else {
      this.isDisabled = false;
    }
  }

  ngOnDestroy() {
    this.meetingSubscription.unsubscribe();
  }

}
