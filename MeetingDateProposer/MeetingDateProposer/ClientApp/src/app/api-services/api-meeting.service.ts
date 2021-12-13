import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AppConfigService } from '../app-config.service';
import { IMeeting } from '../models/meeting.model';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root'
})
export class ApiMeetingService {

  private baseURL: string = AppConfigService.settings.backEndpoint;

  constructor(private http: HttpClient,
    private messageService: MessageService) {

     }

  createMeeting(meeting: IMeeting): Observable<IMeeting> {
    return this.http.post<IMeeting>(`${this.baseURL}/api/CreateMeetingAsync?name=${meeting.name}`,"")
    .pipe(tap(_ => this.log('created Meeting')),
    catchError(this.handleError<IMeeting>()));
  }

  getMeeting(meeting: IMeeting): Observable<IMeeting> {
    return this.http.get<IMeeting>(`${this.baseURL}/api/GetMeetingByIdAsync?meetingId=${meeting.id}`)
    .pipe(tap(_ => this.log('got Meeting')),
    catchError(this.handleError<IMeeting>()));
  }

  updateMeeting(user: IUser, meeting: IMeeting): Observable<IMeeting> {
    return this.http.put<IMeeting>(`${this.baseURL}/api/UpdateMeetingAsync?meetingId=${meeting.id}&userId=${user.id}`,"")
    .pipe(tap(_ => this.log('added user to the meeting')),
    catchError(this.handleError<IMeeting>()));
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
