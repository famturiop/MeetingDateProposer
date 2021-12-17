import { IApiUser } from './api-user.model';

export  interface IApiMeeting {
    id: string;
    connectedUsers: IApiUser[];
    name: string;
  }