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
  public focusedEvent: CalendarEvent = {start: new Date(), title: "", cssClass: ""};
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
        inverseCalendar.push({
          title: "occupied time", 
          start: calendar[i].end as Date, 
          end: calendar[i+1].start,
          cssClass: "calendar-event-occupied-time"});
      }
    }
    return inverseCalendar;
  }

  private setEventsProperties(calendar: CalendarEvent[]): void {
    calendar.forEach(event => {
      event.cssClass = "calendar-event-spare-time";
    });
  }

  ngOnChanges(): void {
    this.calculateAvailableMeetingTime().subscribe(availableTime => {
      this.setEventsProperties(availableTime);
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
    
    event.cssClass = event.cssClass?.concat(" ", "selected");
    this.focusedEvent.cssClass = this.focusedEvent.cssClass?.split(" ")[0];

    if (this.focusedEvent !== event) {
      this.focusedEvent = event;
    }
    else {
      this.calEventDetailsIsActive = false;
      this.focusedEvent = {start: new Date(), title: "", cssClass: ""};
    }
  }
}
