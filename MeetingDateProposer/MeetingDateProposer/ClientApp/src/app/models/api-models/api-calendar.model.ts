import { IApiUser } from './api-user.model';
import { IApiCalendarEvent } from './api-calendar-event.model';

export  interface IApiCalendar {
    id: number;
    user?: IApiUser;
    userId?: number;
    userCalendar: IApiCalendarEvent[];
  }