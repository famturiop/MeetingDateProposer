import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { AppConfigService } from '../app-config.service';
import { IApiMeeting } from '../models/api-models/api-meeting.model';
import { IMeeting } from '../models/meeting.model';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';
import { merge } from 'object-mapper';

@Injectable({
  providedIn: 'root'
})
export class ApiMeetingService {

  private baseURL: string = AppConfigService.settings.backEndpoint;

  private mapFunction = (apiMeeting: IApiMeeting) => {
    console.log(apiMeeting);
    let map = {
      "id": "id",
      "name": "name",
      "connectedUsers[].id": "connectedUsers[].id",
      "connectedUsers[].name": "connectedUsers[].name",
      "connectedUsers[].calendars": "connectedUsers[].calendars",
      "connectedUsers[].calendars[].id": "connectedUsers[].calendars[].id",
      "connectedUsers[].calendars[].userCalendar[].id": "connectedUsers[].calendars[].userCalendar[].id",
      "connectedUsers[].calendars[].userCalendar[].eventStart": "connectedUsers[].calendars[].userCalendar[].start",
      "connectedUsers[].calendars[].userCalendar[].eventEnd": "connectedUsers[].calendars[].userCalendar[].end"
    };
    let meeting: IMeeting = {id: "", connectedUsers: [], name: ""}; 
    merge<IMeeting>(apiMeeting, meeting, map);
    return meeting;
  };

  constructor(private http: HttpClient,
    private messageService: MessageService) {

     }

  createMeeting(meeting: IMeeting): Observable<IMeeting> {
    return this.http.post<IApiMeeting>(`${this.baseURL}/api/CreateMeetingAsync?name=${meeting.name}`,"")
    .pipe(map(this.mapFunction))
    .pipe(tap(_ => this.log('created Meeting')),
    catchError(this.handleError<IMeeting>()));
  }

  getMeeting(meeting: IMeeting): Observable<IMeeting> {
    return this.http.get<IApiMeeting>(`${this.baseURL}/api/GetMeetingByIdAsync?meetingId=${meeting.id}`)
    .pipe(map(this.mapFunction))
    .pipe(tap(_ => this.log('got Meeting')),
    catchError(this.handleError<IMeeting>()));
  }

  updateMeeting(user: IUser, meeting: IMeeting): Observable<IMeeting> {
    return this.http.put<IApiMeeting>(`${this.baseURL}/api/UpdateMeetingAsync?meetingId=${meeting.id}&userId=${user.id}`,"")
    .pipe(map(this.mapFunction))
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
