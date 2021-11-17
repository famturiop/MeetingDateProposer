import { User } from './User';
import { CalendarEvent } from './CalendarEvent';

export  interface Calendar {
    id: number;
    user?: User;
    userId?: number;
    userCalendar: CalendarEvent[];
  }