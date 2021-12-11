import { IUser } from './user.model';

export  interface IMeeting {
    id: string;
    connectedUsers: IUser[];
    name: string;
  }