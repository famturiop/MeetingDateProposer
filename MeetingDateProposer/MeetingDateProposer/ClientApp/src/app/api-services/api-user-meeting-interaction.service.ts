import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AppConfigService } from '../app-config.service';
import { ICalendar } from '../models/calendar.model';
import { IMeeting } from '../models/meeting.model';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root'
})
export class ApiUserMeetingInteractionService {

  private baseURL: string = AppConfigService.settings.backEndpoint;

  constructor(private http: HttpClient,
    private messageService: MessageService) {

     }

  getAvailableMeetingTime(meeting: IMeeting): Observable<ICalendar> {
    return this.http.get<ICalendar>(`${this.baseURL}/api/CalculateMeetingTimeAsync?meetingId=${meeting.id}`)
    .pipe(tap(_ => this.log('calculated available meeting time')),
    catchError(this.handleError<ICalendar>()));
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
