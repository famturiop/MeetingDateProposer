import { Component, HostListener, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApiUserService } from 'src/app/api-services/api-user.service';
import { IMeeting } from 'src/app/models/meeting.model';
import { IUser } from 'src/app/models/user.model';
import { MeetingService } from 'src/app/services/meeting.service';
import { OpenNewWindowService } from 'src/app/services/open-new-window.service';

@Component({
  selector: 'app-connected-user-card',
  templateUrl: './connected-user-card.component.html',
  styleUrls: ['./connected-user-card.component.scss']
})
export class ConnectedUserCardComponent implements OnInit {

  @Input() public user: IUser = {calendars:[], id:"00000000-0000-0000-0000-000000000000", name: ""};
  public addCalendarIsVisible: boolean = false;

  constructor(private newWindow: OpenNewWindowService) {

     }

  ngOnInit(): void {
  }

  addCalendarToUser(user: IUser): void {
    const url = this.newWindow.buildURL(user);
    const name = "";
    this.newWindow.openNewWindow(url,name);
  }

  addCalendarVisibility(): void {
    this.addCalendarIsVisible = this.addCalendarIsVisible ? false : true;
  }

  ngOnDestroy() {
    
  }
}
