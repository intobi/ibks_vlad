export interface TicketReply {
    replyId: number;
    tid: number;
    reply: string;
    replyDate: string;
  }

  export interface CreateTicketReply {
    tid: number;
    reply: string;
  }