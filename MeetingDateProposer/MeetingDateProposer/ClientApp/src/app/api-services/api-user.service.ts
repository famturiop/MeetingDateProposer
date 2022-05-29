import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { IUser } from '../models/user.model';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root'
})
export class ApiUserService {

  constructor(private http: HttpClient,
    private messageService: MessageService,
    @Inject('BASE_URL') private baseUrl: string) {

     }
     
  createUser(user: IUser): Observable<IUser> {
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(user);
    const url = `${this.baseUrl}/api/CreateUser`;

    return this.http.post<IUser>(url,body,{'headers':headers})
    .pipe(catchError(this.handleError<IUser>()));
  }

  getAuthorizationCodeRequest(user: IUser): Observable<string> {
    const url = `${this.baseUrl}/api/GetAuthorizationCodeRequest?userId=${user.id}`;

    return this.http.get<string>(url).pipe(catchError(this.handleError<string>()));
  }

  addGoogleCalendarToUser(user: IUser, authorizationCode: string, isAParticipant?: boolean): Observable<IUser> {
    const url = `${this.baseUrl}/api/AddGoogleCalendarToUser?` +
    `authorizationCode=${authorizationCode}&userId=${user.id}`;

    return this.http.post<IUser>(url,"")
    .pipe(map(user => {
      if (isAParticipant) {
        return this.setUserFlags(user);
      }
      return user;
    }))
    .pipe(catchError(this.handleError<IUser>()));
  }

  private setUserFlags(user: IUser): IUser {
    user.isAParticipant = true;
    return user;
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
