import { Role } from './role';

export class Account {
    id: string;
    title: string;
    fName: string;
    lName: string;
    email: string;
    isEventManager: boolean;
    role: Role;
    jwtToken?: string;
}