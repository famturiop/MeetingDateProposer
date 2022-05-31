import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { CalendarEvent } from 'angular-calendar';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ICalendar } from '../models/calendar.model';
import { IMeeting } from '../models/meeting.model';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root'
})
export class ApiCalculatorService {

  constructor(private http: HttpClient,
    private messageService: MessageService,
    @Inject('BASE_URL') private baseUrl: string) {

     }
  
  getAvailableMeetingTime(calendarsToCompare: ICalendar[]): Observable<CalendarEvent[]> {
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(calendarsToCompare);
    const url = `${this.baseUrl}/api/CalculateMeetingTime`;

    return this.http.post<ICalendar>(url,body,{'headers':headers})
    .pipe(map(this.toAvailableTime))
    .pipe(catchError(this.handleError<CalendarEvent[]>()));
  }

  private toAvailableTime(apiCalendar: ICalendar): CalendarEvent[] {
    let availableTime: CalendarEvent[] = apiCalendar.userCalendar; 

    availableTime.forEach(calEvent => {
      calEvent.start = new Date(calEvent.start);
      calEvent.end = new Date(calEvent.end!);
      calEvent.title = "spare time";
    });
    
    return availableTime;
  };

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
  
      return of(result as T);
    };
  }

  private log(message: string) {
    this.messageService.add(`api-calculator.service: ${message}`);
  }
}
