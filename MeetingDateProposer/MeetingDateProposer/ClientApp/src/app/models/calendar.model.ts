import { IUser } from './user.model';
import { ICalendarEvent } from './calendar-event.model';

export  interface ICalendar {
    id: number;
    user?: IUser;
    userId?: number;
    userCalendar: ICalendarEvent[];
  }