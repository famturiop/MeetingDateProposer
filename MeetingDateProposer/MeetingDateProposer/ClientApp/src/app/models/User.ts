import { Calendar } from './Calendar';
import { Meeting } from './Meeting';

export interface User {
    credentials?: any;
    calendars: Calendar[];
    id: string;
    userMeetings?: Meeting[];
    name: string;
  }