import { IMeeting } from '../models/meeting.model';

import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

import { MessageService } from '../services/message.service';
import { AppConfigService } from '../app-config.service';

@Injectable({
  providedIn: 'root'
})
export class StageOneService {

  private baseURL: string = AppConfigService.settings.backEndpoint;

  constructor(private http: HttpClient,
    private messageService: MessageService) {

     }

  createMeeting(meeting: IMeeting): Observable<IMeeting> {
    return this.http.post<IMeeting>(`${this.baseURL}/api/CreateMeetingAsync?name=${meeting.name}`,"")
    .pipe(tap(_ => this.log('created Meeting')),
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
