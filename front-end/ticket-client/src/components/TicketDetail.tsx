import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { createTicketReply, getTicketDetails, getTicketReplies, updateTicket, updateTicketReply } from "../services/ApiService";
import { useNavigate } from "react-router-dom";
import { SelectChangeEvent } from "@mui/material";
import { CreateTicketReply, TicketReply } from "../models/TicketReply";
import {
  Box,
  Typography,
  CircularProgress,
  Button,
  FormControl,
  Select,
  InputLabel,
  MenuItem,
  TextField,
} from "@mui/material";
import { UpdateTicket } from "../models/Ticket";
import { TicketDetails } from "../models/TicketDetail";
import { isAxiosError } from "axios";

const TicketDetail: React.FC = () => {
  const { id } = useParams();
  const [ticket, setTicket] = useState<TicketDetails | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [newReply, setNewReply] = useState("");
  const [type, setType] = useState(0);
  const [state, setState] = useState(0);
  const [description, setDescription] = useState("");
  const [replies, setReplies] = useState<TicketReply[]>([]);

  const [module, setModule] = useState('');
  const [urgentLevel, setUrgentLevel] = useState<number>(0);
  const [editReply, setEditReply] = useState<TicketReply | null>(null);
  const [editedReplyText, setEditedReplyText] = useState<string>('');

  const [errors, setErrors] = useState<{ Description?: string }>({});

  const navigate = useNavigate();

  useEffect(() => {
    const fetchTicket = async () => {
      setLoading(true);
      try {
        const data = await getTicketDetails(id!);
        setTicket(data);
        setModule(data.applicationName);
        setUrgentLevel(data.priorityId);
        setType(data.ticketTypeId);
        setState(data.statusId);
        setState(data.statusId);
        setDescription(data.description)
        const repliesData = await getTicketReplies(data.id);
        setReplies(repliesData);
      } catch (error) {
        console.error("Error fetching ticket details:", error);
      }
      setLoading(false);
    };

    if (id) {
      fetchTicket();
    }
  }, [id]);

  if (loading) return <CircularProgress />;
  if (!ticket) return <Typography variant="h6">Ticket not found</Typography>;

  const handleModuleChange = (event: SelectChangeEvent<string>) => {
    setModule(event.target.value);
  };

  const handleUrgentLevelChange = (event: SelectChangeEvent<number>) => {
    setUrgentLevel(Number(event.target.value));
  };

  const handleTypeChange = (event: SelectChangeEvent<number>) => {
    setType(Number(event.target.value));
  };

  const handleStateChange = (event: SelectChangeEvent<number>) => {
    setState(Number(event.target.value));
  };

  const updateTicketHandler = async () => {
    const updatedTicket: UpdateTicket = {
      applicationName: module,
      priorityId: urgentLevel,
      ticketTypeId: type,
      description: description
    };

    try {
      setErrors({});

      await updateTicket(updatedTicket, ticket.id);

      alert("Ticket updated successfully");
      navigate("/");
    } catch (error: unknown) {
      console.error("Error updating ticket:", error);
  
      if (isAxiosError(error)) {
        if (error.response && error.response.data.errors) {
          console.error('Response data errors:', error.response.data.errors);
          setErrors(error.response.data.errors);
        } else {
          alert("Error updating ticket: " + error.message);
        }
      } else {
        alert("Unexpected error: " + error);
      }
    }
  };

  const addReplyHandler = async () => {
    const newTicketReply: CreateTicketReply = {
      tid: ticket.id,
      reply: newReply
    };

    try {
      const createdReply = await createTicketReply(newTicketReply);

      const replyWithId: TicketReply = {
        replyId: createdReply.replyId,
        tid: newTicketReply.tid,
        reply: newTicketReply.reply,
        replyDate: new Date().toISOString()
      };

      setReplies(prevReplies => [...prevReplies, replyWithId]);
      setNewReply("");
      alert("Reply added successfully");
    } catch (error) {
      console.error("Error adding reply:", error);
      alert("Error adding reply");
    }
  };

  const handleEditReplyChange = (reply: TicketReply) => {
    setEditReply(reply);
    setEditedReplyText(reply.reply);
  };

  const handleSaveEditReply = async () => {
    if (editReply) {
      const updatedReply = { ...editReply, reply: editedReplyText };
      try {
        await updateTicketReply(updatedReply);
        setReplies(prevReplies =>
          prevReplies.map((reply) =>
            reply.replyId === updatedReply.replyId ? updatedReply : reply
          )
        );
        setEditReply(null);
        setEditedReplyText('');
        alert("Reply updated successfully");
      } catch (error) {
        console.error("Error updating reply:", error);
        alert("Error updating reply");
      }
    }
  };


  return (
    <Box display="flex" flexDirection="column" m={2}>
      <Box display="flex" alignItems="center" justifyContent="space-between" mb={2}>
        <Typography variant="h5">Ticket #</Typography>
        <Typography variant="h5">
          {ticket.id} {ticket.title}
        </Typography>
        <Box>
          <Button
            variant="contained"
            color="secondary"
            sx={{ mr: 1 }}
            onClick={() => navigate('/')}
          >
            Close
          </Button>
          <Button variant="contained" color="primary" onClick={updateTicketHandler}>
            Save
          </Button>
        </Box>
      </Box>

      <Box mb={3}>
        <TextField
          label="New Reply"
          value={newReply}
          onChange={(e) => setNewReply(e.target.value)}
          multiline
          rows={4}
          fullWidth
        />
        <Button
          variant="contained"
          color="primary"
          onClick={addReplyHandler}
          sx={{ mt: 2 }}
          disabled={!newReply.trim()}
        >
          Add Reply
        </Button>
      </Box>

      <Box display="flex" gap={2}>
        <Box flex="1" display="flex" flexDirection="column" gap={2}>
          <FormControl sx={{ minWidth: 150 }}>
            <InputLabel>Module</InputLabel>
            <Select value={module || ''} onChange={handleModuleChange} label="Module">
              <MenuItem value="Loader">Loader</MenuItem>
              <MenuItem value="HR">HR</MenuItem>
              <MenuItem value="Finance">Finance</MenuItem>
              <MenuItem value="Ingress">Ingress</MenuItem>
              <MenuItem value="Clusters">Clusters</MenuItem>
            </Select>
          </FormControl>

          <FormControl sx={{ minWidth: 150 }}>
            <InputLabel>Urgent LVL</InputLabel>
            <Select value={urgentLevel} onChange={handleUrgentLevelChange} label="Urgent LVL">
              <MenuItem value={1}>Low</MenuItem>
              <MenuItem value={2}>Medium</MenuItem>
              <MenuItem value={3}>High</MenuItem>
              <MenuItem value={4}>Priority</MenuItem>
              <MenuItem value={5}>None</MenuItem>
            </Select>
          </FormControl>

          <FormControl sx={{ minWidth: 150 }}>
            <InputLabel>Type</InputLabel>
            <Select value={type} onChange={handleTypeChange} label="Type">
              <MenuItem value={1}>Question</MenuItem>
              <MenuItem value={2}>Issue</MenuItem>
              <MenuItem value={3}>Suggestion</MenuItem>
              <MenuItem value={4}>Feedback</MenuItem>
            </Select>
          </FormControl>

          <FormControl sx={{ minWidth: 150 }}>
            <InputLabel>State</InputLabel>
            <Select value={state} label="State" disabled>
              <MenuItem value={1}>New</MenuItem>
              <MenuItem value={2}>Open</MenuItem>
              <MenuItem value={3}>Awaiting Response</MenuItem>
              <MenuItem value={6}>Closed</MenuItem>
            </Select>
          </FormControl>

          <TextField
            label="Description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            multiline
            rows={4}
            fullWidth
            sx={{ mt: 2 }}
            error={Boolean(errors.Description)}
            helperText={errors.Description}
          />
        </Box>

        <Box flex="1" display="flex" flexDirection="column" gap={2} sx={{ maxHeight: 500, overflowY: 'auto' }}>
          {replies.map((reply) => (
            <Box key={reply.replyId} p={2} border="1px solid #ccc" borderRadius={2}>
              <Typography variant="body1" fontWeight="bold">
                Reply #{reply.replyId}
              </Typography>
              {editReply?.replyId === reply.replyId ? (
                <Box>
                  <TextField
                    label="Edit Reply"
                    value={editedReplyText}
                    onChange={(e) => setEditedReplyText(e.target.value)}
                    multiline
                    rows={4}
                    fullWidth
                  />
                  <Button
                    variant="contained"
                    color="primary"
                    onClick={handleSaveEditReply}
                    sx={{ mt: 2 }}
                  >
                    Save
                  </Button>
                </Box>
              ) : (
                <Typography variant="body2">{reply.reply}</Typography>
              )}
              <Button
                variant="outlined"
                color="primary"
                onClick={() => handleEditReplyChange(reply)}
                sx={{ mt: 2 }}
              >
                Edit
              </Button>
            </Box>
          ))}
        </Box>

      </Box>
    </Box>
  );
};

export default TicketDetail;
