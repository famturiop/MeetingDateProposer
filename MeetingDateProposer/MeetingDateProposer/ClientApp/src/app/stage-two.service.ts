import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { BackendBaseService } from './backend-base.service';
import { Meeting } from './domain-objects/Meeting';
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
