import { Calendar } from './Calendar';
import { Meeting } from './Meeting';

export interface User {
    credentials?: any;
    calendars: Calendar[];
    userMeetings?: Meeting[];
    id: number;
  }