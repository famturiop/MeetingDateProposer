import { Injectable } from '@angular/core';
import { IMeeting } from '../models/meeting.model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {

  private meeting = new BehaviorSubject<IMeeting>({id: "00000000-0000-0000-0000-000000000000", connectedUsers: [], name: ""});
  currentMeeting = this.meeting.asObservable();

  constructor() { }

  updateMeeting(meeting: IMeeting){
    if (meeting.connectedUsers?.length > 1){
      meeting.connectedUsers?.sort((userA,userB) => {
        return userA.name.localeCompare(userB.name);
      })
    }
    this.meeting.next(meeting);
  }
}
