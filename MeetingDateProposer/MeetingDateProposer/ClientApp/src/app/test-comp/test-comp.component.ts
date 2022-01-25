import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-test-comp',
  templateUrl: './test-comp.component.html',
  styleUrls: ['./test-comp.component.css']
})
export class TestCompComponent {

  public user: User;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this.testMethod(baseUrl); }

  testMethod(baseUrl: string) {
    //http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    //  http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
    //    this.forecasts = result;
    //  }, error => console.error(error)

    this.http.get<User>(baseUrl + 't').subscribe(result => { this.user = result; }, error => console.error(error));
    //this.user.calendars[0].userCalendar[0].eventStart
  }

  ngOnInit() {
  }

}

interface User {
  credentials: any;
  calendars: Calendar[];
  userMeetings: Meeting[];
  id: number;
}

interface Calendar {
  id: number;
  user: User;
  userId: number;
  userCalendar: CalendarEvent[];
}

interface Meeting {
  id: number;
  connectedUsers: User[];
}

interface CalendarEvent {
  id: number;
  calendarId: number;
  calendar: Calendar;
  eventStart: Date;
  eventEnd: Date;
}

