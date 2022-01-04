import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { CalendarEvent, CalendarUtils, CalendarView } from 'angular-calendar';
import { Observable, Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ApiUserMeetingInteractionService } from 'src/app/api-services/api-user-meeting-interaction.service';
import { ICalendar } from 'src/app/models/calendar.model';
import { IMeeting } from 'src/app/models/meeting.model';
import { MeetingService } from 'src/app/services/meeting.service';

@Component({
  selector: 'app-meeting-joint-calendar',
  templateUrl: './meeting-joint-calendar.component.html',
  styleUrls: ['./meeting-joint-calendar.component.scss']
})
export class MeetingJointCalendarComponent implements OnInit, OnChanges {

  @Input() public meeting: IMeeting = {id: "00000000-0000-0000-0000-000000000000", connectedUsers: [], name: ""};
  public currentDate: Date = new Date();
  public displayCalendar: CalendarEvent[] = [];
  view: CalendarView = CalendarView.Week;
  activeDayIsOpen: boolean = true;

  constructor(private apiUserMeetingInteractionService: ApiUserMeetingInteractionService) {
   }

  ngOnInit(): void {

  }

  private calculateAvailableMeetingTime(): Observable<CalendarEvent[]> {
    return this.apiUserMeetingInteractionService.getAvailableMeetingTime(this.meeting);
  }

  private inverseCalendar(calendar: CalendarEvent[]): CalendarEvent[] {
    let inverseCalendar: CalendarEvent[] = [];
    if (calendar.length > 1) {
      for (let i = 0; i < (calendar.length - 1); i++) {
        inverseCalendar.push({title: "busy time", start: calendar[i].end as Date, end: calendar[i+1].start});
      }
    }
    return inverseCalendar;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }

  ngOnChanges(): void {
    this.calculateAvailableMeetingTime().subscribe(availableTime => {
      let inverseAvailableTime = this.inverseCalendar(availableTime);
      this.displayCalendar = availableTime.concat(inverseAvailableTime);
    })
  }

  ngOnDestroy(): void {

  }
}
