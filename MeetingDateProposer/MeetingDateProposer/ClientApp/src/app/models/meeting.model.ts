import { CalendarEvent } from 'angular-calendar';
import { IUser } from './user.model';

export  interface IMeeting {
    id: string;
    connectedUsers: IUser[];
    name: string;
    timeInterval?: Date;
    availableTime?: CalendarEvent[];
  }