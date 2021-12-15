import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { CalendarEvent } from 'angular-calendar';
import { Subscription } from 'rxjs';
import { ApiUserMeetingInteractionService } from 'src/app/api-services/api-user-meeting-interaction.service';
import { ICalendar } from 'src/app/models/calendar.model';
import { IMeeting } from 'src/app/models/meeting.model';
import { MeetingService } from 'src/app/services/meeting.service';

@Component({
  selector: 'app-meeting-joint-calendar',
  templateUrl: './meeting-joint-calendar.component.html',
  styleUrls: ['./meeting-joint-calendar.component.css']
})
export class MeetingJointCalendarComponent implements OnInit, OnChanges {

  @Input() public meeting: IMeeting = {id: "", connectedUsers: [], name: ""};
  public viewDate: Date = new Date();
  public availableTime: CalendarEvent[] = [];

  constructor(private apiUserMeetingInteractionService: ApiUserMeetingInteractionService) {
   }

  ngOnInit(): void {

  }

  private calculateAvailableMeetingTime(): void {
    this.apiUserMeetingInteractionService.getAvailableMeetingTime(this.meeting).subscribe((response) => {
      this.convertCalendar(response);
    });
  }

  private convertCalendar(calendar: ICalendar): void {
    let calendarEvents: CalendarEvent[] = [];
    calendar.userCalendar.forEach((iCalendarEvent) => {
      let calendarEvent: CalendarEvent = { start: new Date(), title: ""};
      calendarEvent.start = new Date(iCalendarEvent.eventStart);
      calendarEvent.end = new Date(iCalendarEvent.eventEnd);
      calendarEvent.title = "";
      calendarEvents.push(calendarEvent);
    })
    this.availableTime = calendarEvents;
  }

  ngOnChanges(): void {
    this.calculateAvailableMeetingTime();
  }

  ngOnDestroy(): void {

  }
}
