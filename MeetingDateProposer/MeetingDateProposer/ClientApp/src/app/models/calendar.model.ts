import { IUser } from './user.model';
import { ICalendarEvent } from './calendar-event.model';
import { CalendarEvent } from 'angular-calendar';

export  interface ICalendar {
    id: number;
    user?: IUser;
    userId?: number;
    userCalendar: ICalendarEvent[];
  }