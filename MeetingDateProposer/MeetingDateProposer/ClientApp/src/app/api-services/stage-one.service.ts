import { Meeting } from '../models/Meeting';

import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

import { MessageService } from '../services/message.service';
import { BackendBaseService } from './backend-base.service';

@Injectable({
  providedIn: 'root'
})
export class StageOneService extends BackendBaseService {

  constructor(private http: HttpClient,
    private messageService: MessageService) {
      super();
     }
  
  private access_url = 'api/calendar';

  createMeeting(meeting: Meeting): Observable<Meeting> {
    return this.http.post<Meeting>(`${this.baseURL}/api/CreateMeetingAsync?name=${meeting.name}`,"")
    .pipe(tap(_ => this.log('created Meeting')),
    catchError(this.handleError<Meeting>()));
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
