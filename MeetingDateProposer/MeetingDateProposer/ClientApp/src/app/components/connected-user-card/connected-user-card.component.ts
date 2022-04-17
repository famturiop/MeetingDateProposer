import { Component, Input, OnInit } from '@angular/core';
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
  public addCalendarIsVisible: boolean = false;
  public readonly iconUrl: string = "/assets/Google_Calendar_icon_(2020).svg";

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
}
