import { ICalendar } from './calendar.model';
import { IMeeting } from './meeting.model';

export interface IUser {
    credentials?: any;
    calendars: ICalendar[];
    id: string;
    userMeetings?: IMeeting[];
    name: string;
  }