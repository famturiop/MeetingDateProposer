import { User } from './User';

export  interface Meeting {
    id: string;
    connectedUsers: User[];
    name: string;
  }