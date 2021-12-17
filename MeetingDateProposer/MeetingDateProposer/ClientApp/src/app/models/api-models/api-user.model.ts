import { IApiCalendar } from './api-calendar.model';
import { IApiMeeting } from './api-meeting.model';

export interface IApiUser {
    credentials?: any;
    calendars: IApiCalendar[];
    id: string;
    userMeetings?: IApiMeeting[];
    name: string;
  }