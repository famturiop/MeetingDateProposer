import { IApiCalendar } from './api-calendar.model';

export  interface IApiCalendarEvent {
    id: number;
    calendarId: number;
    calendar?: IApiCalendar;
    eventStart: Date;
    eventEnd: Date;
  }