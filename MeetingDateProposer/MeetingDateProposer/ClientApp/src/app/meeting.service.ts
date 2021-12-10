import { Injectable } from '@angular/core';
import { Meeting } from './models/Meeting';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {

  private meeting = new BehaviorSubject<Meeting>({id: "", connectedUsers: [], name: ""});
  currentMeeting = this.meeting.asObservable();

  constructor() { }

  updateMeeting(meeting: Meeting){
    if (meeting.connectedUsers?.length > 1){
      meeting.connectedUsers?.sort((userA,userB) => {
        return userA.name.localeCompare(userB.name);
      })
    }
    this.meeting.next(meeting);
  }
}
