import { User } from './domain-objects/User';
import { Calendar } from './domain-objects/Calendar';

import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

import { MessageService } from './message.service';
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

  getCalendar(): Observable<User> {
    return this.http.get<User>(`${this.baseURL}/api/calendar`).pipe(
      tap(_ => this.log('fetched Calendar')),
    catchError(this.handleError<User>('getCalendar')));
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
