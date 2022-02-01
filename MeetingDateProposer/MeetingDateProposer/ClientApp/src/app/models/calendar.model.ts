import { IUser } from './user.model';
import { CalendarEvent } from 'angular-calendar';

export  interface ICalendar {
    id: number;
    userCalendar: CalendarEvent[];
  }