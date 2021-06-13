export interface User {
    credentials: any;
    calendars: Calendar[];
    userMeetings: Meeting[];
    id: number;
  }
  
export  interface Calendar {
    id: number;
    user: User;
    userId: number;
    userCalendar: CalendarEvent[];
  }
  
export  interface Meeting {
    id: number;
    connectedUsers: User[];
  }
  
export  interface CalendarEvent {
    id: number;
    calendarId: number;
    calendar: Calendar;
    eventStart: Date;
    eventEnd: Date;
  }