import { Calendar } from './Calendar';

export  interface CalendarEvent {
    id: number;
    calendarId: number;
    calendar?: Calendar;
    eventStart: Date;
    eventEnd: Date;
  }