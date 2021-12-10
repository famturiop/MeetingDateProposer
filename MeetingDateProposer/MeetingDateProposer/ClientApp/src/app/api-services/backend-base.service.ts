import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BackendBaseService {

  public static  readonly baseURL: string = 'https://localhost:44343';

  constructor(){}
}
