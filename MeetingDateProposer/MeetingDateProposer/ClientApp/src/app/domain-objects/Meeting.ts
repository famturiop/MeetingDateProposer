import { User } from './User';

export  interface Meeting {
    id: number;
    connectedUsers: User[];
  }