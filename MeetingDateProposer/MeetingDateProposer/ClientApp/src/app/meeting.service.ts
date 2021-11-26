import { Injectable } from '@angular/core';
import { Meeting } from './domain-objects/Meeting';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {

  private meeting = new BehaviorSubject<Meeting>({id: "", connectedUsers: [], name: "hai"});
  currentMeeting = this.meeting.asObservable();

  constructor() { }

  updateMeeting(meeting: Meeting){
    this.meeting.next(meeting);
  }
}
