import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ApiUserService } from 'src/app/api-services/api-user.service';
import { IUser } from 'src/app/models/user.model';
import { OpenNewWindowService } from 'src/app/services/open-new-window.service';

@Component({
  selector: 'app-connected-user-card',
  templateUrl: './connected-user-card.component.html',
  styleUrls: ['./connected-user-card.component.scss'],
  providers: [OpenNewWindowService]
})
export class ConnectedUserCardComponent implements OnInit {

  @Input() public user: IUser = {calendars:[], id:"", name: ""};
  @Output() participationChanged: EventEmitter<IUser> =  new EventEmitter();
  
  public addCalendarIsVisible: boolean = false;
  public readonly iconUrl: string = "/assets/Google_Calendar_icon_(2020).svg";

  constructor(
    private newWindow: OpenNewWindowService,
    private apiUserService: ApiUserService) {
  }

  ngOnInit(): void {
  }

  addCalendarToUser(user: IUser): void {
    this.apiUserService.getAuthorizationCodeRequest(user).subscribe((response) => {
      const url = response;
      const name = "";
      this.newWindow.openNewWindow(url,name);
    })
  }

  addCalendarVisibility(): void {
    this.addCalendarIsVisible = this.addCalendarIsVisible ? false : true;
  }

  isAParticipant(): void {
    this.user.isAParticipant = this.user.isAParticipant ? false : true;
    this.participationChanged.emit(this.user);
  }
}
