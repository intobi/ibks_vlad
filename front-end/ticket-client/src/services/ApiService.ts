import axios from 'axios';
import { Ticket, UpdateTicket } from '../models/Ticket';
import { CreateTicketReply, TicketReply } from '../models/TicketReply';

const API_URL = 'http://localhost:5117/api';

const api = axios.create({
  baseURL: API_URL,
});

export const getTickets = async (): Promise<Ticket[]> => {
    try {
      const response = await api.get('/Ticket');
      return response.data;
    } catch (error) {
      console.error('Error fetching tickets:', error);
      throw error;
    }
  };
  
  export const getTicketDetails = async (id: any): Promise<Ticket> => {
    try {
      const response = await api.get(`/Ticket/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Error fetching ticket ${id}:`, error);
      throw error;
    }
  };

  export const getTicketReplies = async (ticketId: number): Promise<TicketReply[]> => {
    try {
      const response = await api.get(`/TicketReply/${ticketId}`);
      return response.data;
    } catch (error) {
      console.error(`Error fetching replies for ticket ${ticketId}:`, error);
      throw error;
    }
  };

  export const updateTicket = async (ticket: UpdateTicket, ticketId: number) => {
    try {
      const response = await api.put(`/Ticket/${ticketId}`, ticket);
      return response.data;
    } catch (error) {
      console.error('Error updating ticket:', error);
      throw error;
    }
  };

  export const createTicketReply = async (reply: CreateTicketReply) => {
    try {
      const response = await api.post(`/TicketReply`, reply);
      return response.data;
    } catch (error) {
      console.error('Error creating reply:', error);
      throw error;
    }
  };

  export const updateTicketReply = async (reply: TicketReply) => {
    try {
      const response = await api.put(`/TicketReply/${reply.replyId}`, reply);
      return response.data;
    } catch (error) {
      console.error('Error updating reply:', error);
      throw error;
    }
  };
  