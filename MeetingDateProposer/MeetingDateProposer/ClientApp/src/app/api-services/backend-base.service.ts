import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BackendBaseService {

  baseURL: string = 'https://localhost:44343';

  constructor(){}
}
