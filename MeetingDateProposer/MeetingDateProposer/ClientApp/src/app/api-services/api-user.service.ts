import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { AppConfigService } from '../app-config.service';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';
import { merge } from 'object-mapper';

@Injectable({
  providedIn: 'root'
})
export class ApiUserService {

  private baseURL: string = AppConfigService.settings.backEndpoint;

  constructor(private http: HttpClient,
    private messageService: MessageService) {

     }
     
  createUser(user: IUser): Observable<IUser> {
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(user);
    return this.http.post<IUser>(`${this.baseURL}/api/CreateUserAsync`,body,{'headers':headers})
    .pipe(map(this.mapFunction))
    .pipe(tap(_ => this.log('created User')),
    catchError(this.handleError<IUser>()));
  }

  updateUser(user: IUser, authorizationCode: string): Observable<IUser> {
    return this.http.put<IUser>(`${this.baseURL}/api/AddCalendarToUserAsync?authorizationCode=${authorizationCode}&userId=${user.id}`,"")
    .pipe(map(this.mapFunction))
    .pipe(tap(_ => this.log('added user to the meeting')),
    catchError(this.handleError<IUser>()));
  }

  private mapFunction(apiUser: IUser): IUser {
    return apiUser;
  };

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