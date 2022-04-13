import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AppConfigService } from '../app-config.service';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root'
})
export class ApiUserService {

  private readonly baseURL: string = AppConfigService.settings.backEndpoint;

  constructor(private http: HttpClient,
    private messageService: MessageService) {

     }
     
  createUser(user: IUser): Observable<IUser> {
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(user);
    const url = `${this.baseURL}/api/CreateUser`;

    return this.http.post<IUser>(url,body,{'headers':headers})
    .pipe(catchError(this.handleError<IUser>()));
  }

  addGoogleCalendarToUser(user: IUser, authorizationCode: string): Observable<IUser> {
    const url = `${this.baseURL}/api/AddGoogleCalendarToUser?` +
    `authorizationCode=${authorizationCode}&userId=${user.id}`;

    return this.http.post<IUser>(url,"")
    .pipe(catchError(this.handleError<IUser>()));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
  
      return of(result as T);
    };
  }

  private log(message: string) {
    this.messageService.add(`api-user.service: ${message}`);
  }
}
