import { ICalendar } from './calendar.model';

export  interface ICalendarEvent {
    id: number;
    calendarId: number;
    calendar?: ICalendar;
    eventStart: Date;
    eventEnd: Date;
  }