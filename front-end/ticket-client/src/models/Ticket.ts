export interface Ticket {
    id: number;
    title: string;
    applicationId: number;
    applicationName: string;
    description: string;
    priorityId: number;
    statusId: number;
    userId: number;
    userOid: string;
    installedEnvironmentId: number;
    ticketTypeId: number;
    date: string;
    lastModified: string;
  }
  
  export interface UpdateTicket {
    applicationName: string;
    priorityId: number;
    ticketTypeId: number;
    description: string;
  }
  