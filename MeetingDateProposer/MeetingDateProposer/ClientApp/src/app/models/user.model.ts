import { ICalendar } from './calendar.model';
import { IMeeting } from './meeting.model';

export interface IUser {
    id: string;
    name: string;
    calendars: ICalendar[];
    isAParticipant?: boolean;
  }