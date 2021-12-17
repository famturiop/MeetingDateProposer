import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CalendarEvent } from 'angular-calendar';
import { EventColor } from 'calendar-utils';
import { merge } from 'object-mapper';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { AppConfigService } from '../app-config.service';
import { IApiCalendar } from '../models/api-models/api-calendar.model';
import { ICalendar } from '../models/calendar.model';
import { IMeeting } from '../models/meeting.model';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root'
})
export class ApiUserMeetingInteractionService {

  private baseURL: string = AppConfigService.settings.backEndpoint;

  private mapFunction = (apiCalendar: IApiCalendar) => {
    let map = {
      "userCalendar[].eventStart": "[].start",
      "userCalendar[].eventEnd": "[].end",
      };
    let availableTime: CalendarEvent[] = []; 
    merge<CalendarEvent[]>(apiCalendar, availableTime, map);
    availableTime.forEach(calEvent => {
      calEvent.start = new Date(calEvent.start);
      calEvent.end = new Date(calEvent.end as Date);
      calEvent.color = {primary: "pink", secondary: "green"};
      calEvent.title = "available free time";
    });
    console.log(availableTime);
    return availableTime;
  };

  private mapFunctionOld = (calendar: IApiCalendar) => {
    let availableTime: CalendarEvent[] = [];
    calendar.userCalendar.forEach((iCalendarEvent) => {
      let calendarEvent: CalendarEvent = { start: new Date(), title: ""};
      calendarEvent.start = new Date(iCalendarEvent.eventStart);
      calendarEvent.end = new Date(iCalendarEvent.eventEnd);
      calendarEvent.title = "available free time";
      availableTime.push(calendarEvent);
    })
    return availableTime;
  }

  constructor(private http: HttpClient,
    private messageService: MessageService) {

     }
  
  getAvailableMeetingTime(meeting: IMeeting): Observable<CalendarEvent[]> {
    return this.http.get<IApiCalendar>(`${this.baseURL}/api/CalculateMeetingTimeAsync?meetingId=${meeting.id}`)
    .pipe(map(this.mapFunction))
    .pipe(tap(_ => this.log('calculated available meeting time')),
    catchError(this.handleError<CalendarEvent[]>()));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    this.messageService.add(`stage-one.service: ${message}`);
  }
}
