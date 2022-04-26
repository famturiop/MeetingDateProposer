import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IMeeting } from '../models/meeting.model';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root'
})
export class ApiMeetingService {

  constructor(private http: HttpClient,
    private messageService: MessageService,
    @Inject('BASE_URL') private baseUrl: string) {

     }

  createMeeting(meeting: IMeeting): Observable<IMeeting> {
    const url = `${this.baseUrl}/api/CreateMeeting`;
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(meeting);

    return this.http.post<IMeeting>(url, body, {'headers':headers})
    .pipe(catchError(this.handleError<IMeeting>()));
  }

  getMeeting(meeting: IMeeting): Observable<IMeeting> {
    const url = `${this.baseUrl}/api/GetMeetingById?meetingId=${meeting.id}`;

    return this.http.get<IMeeting>(url)
    .pipe(catchError(this.handleError<IMeeting>()));
  }

  addUserToMeeting(user: IUser, meeting: IMeeting): Observable<IMeeting> {
    const url = `${this.baseUrl}/api/AddUserToMeeting?meetingId=${meeting.id}`;
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(user);

    return this.http.patch<IMeeting>(url, body, {'headers':headers})
    .pipe(catchError(this.handleError<IMeeting>()));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }

  private log(message: string) {
    this.messageService.add(`api-meeting.service: ${message}`);
  }
}
