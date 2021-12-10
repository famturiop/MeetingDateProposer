import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, filter, map, tap } from 'rxjs/operators';
import { BackendBaseService } from './backend-base.service';
import { Meeting } from './models/Meeting';
import { User } from './models/User';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class StageTwoService extends BackendBaseService {

  constructor(private http: HttpClient,
    private messageService: MessageService) { 
    super()
  }

  

  getMeeting(meeting: Meeting): Observable<Meeting> {
    return this.http.get<Meeting>(`${this.baseURL}/api/GetMeetingByIdAsync?meetingId=${meeting.id}`)
    .pipe(tap(_ => this.log('got Meeting')),
    catchError(this.handleError<Meeting>()));
  }

  createUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.baseURL}/api/CreateUserAsync?name=${user.name}`,"")
    .pipe(tap(_ => this.log('created User')),
    catchError(this.handleError<User>()));
  }

  updateMeeting(user: User, meeting: Meeting): Observable<Meeting> {
    return this.http.put<Meeting>(`${this.baseURL}/api/UpdateMeetingAsync?meetingId=${meeting.id}&userId=${user.id}`,"")
    .pipe(tap(_ => this.log('added user to the meeting')),
    catchError(this.handleError<Meeting>()));
  }

  updateUser(user: User, authorizationCode: string): Observable<User> {
    return this.http.put<User>(`${this.baseURL}/api/AddCalendarToUserAsync?authorizationCode=${authorizationCode}&userId=${user.id}`,"")
    .pipe(tap(_ => this.log('added user to the meeting')),
    catchError(this.handleError<User>()));
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
