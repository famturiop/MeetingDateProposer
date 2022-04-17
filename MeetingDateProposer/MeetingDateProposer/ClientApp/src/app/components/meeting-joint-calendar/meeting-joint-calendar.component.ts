import { Component, HostListener, Input, OnChanges, OnInit } from '@angular/core';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { Observable } from 'rxjs';
import { ApiCalculatorService } from 'src/app/api-services/api-calculator.service';
import { IMeeting } from 'src/app/models/meeting.model';

@Component({
  selector: 'app-meeting-joint-calendar',
  templateUrl: './meeting-joint-calendar.component.html',
  styleUrls: ['./meeting-joint-calendar.component.scss']
})
export class MeetingJointCalendarComponent implements OnInit, OnChanges {

  @Input() public meeting: IMeeting = {id: "", connectedUsers: [], name: ""};
  public viewDate: Date = new Date();
  public displayCalendar: CalendarEvent[] = [];
  public view: CalendarView = CalendarView.Week;
  public calendarView = CalendarView;
  public readonly minimumEventHeight = 0;
  public focusedEvent: CalendarEvent = {start: new Date(), title: ""};
  public calEventDetailsIsActive: boolean = false;
  public readonly weekStartsOn: number = 1;
  private readonly viewSwitchInnerWidth: number = 450;

  constructor(private apiCalculatorService: ApiCalculatorService,
    private window: Window) {
  
  }

  ngOnInit(): void {
    if (this.window.innerWidth <= this.viewSwitchInnerWidth) {
      this.view = CalendarView.Day;
    }
    else {
      this.view = CalendarView.Week;
    }
  }

  private calculateAvailableMeetingTime(): Observable<CalendarEvent[]> {
    return this.apiCalculatorService.getAvailableMeetingTime(this.meeting);
  }

  private inverseCalendar(calendar: CalendarEvent[]): CalendarEvent[] {
    let inverseCalendar: CalendarEvent[] = [];
    if (calendar.length > 1) {
      for (let i = 0; i < (calendar.length - 1); i++) {
        inverseCalendar.push({title: "occupied time", start: calendar[i].end as Date, end: calendar[i+1].start});
      }
    }
    return inverseCalendar;
  }

  ngOnChanges(): void {
    this.calculateAvailableMeetingTime().subscribe(availableTime => {
      let inverseAvailableTime = this.inverseCalendar(availableTime);
      this.displayCalendar = availableTime.concat(inverseAvailableTime);
    })
  }

  @HostListener('window:resize',['$event']) onScreenSizeChange(event: any): void {
    if (event.target.innerWidth <= this.viewSwitchInnerWidth) {
      this.view = CalendarView.Day;
    }
    else {
      this.view = CalendarView.Week;
    }
  }

  eventClicked({ event }: { event: CalendarEvent }): void {
    this.calEventDetailsIsActive = true;
    this.focusedEvent = event;
  }
}
