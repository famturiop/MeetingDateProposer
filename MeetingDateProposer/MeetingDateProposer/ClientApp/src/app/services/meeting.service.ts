import { Injectable } from '@angular/core';
import { IMeeting } from '../models/meeting.model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {

  private meeting = new BehaviorSubject<IMeeting>({id: "", connectedUsers: [], name: ""});
  currentMeeting = this.meeting.asObservable();

  constructor() { }

  updateMeeting(meeting: IMeeting){
    this.meeting.next(meeting);
  }

}
