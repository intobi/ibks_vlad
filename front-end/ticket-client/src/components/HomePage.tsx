import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getTickets } from "../services/ApiService";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TablePagination,
  CircularProgress,
  Typography,
  Box,
  Button,
  Paper,
  Grid,
} from "@mui/material";
import { Ticket } from "../models/Ticket";
import PriorityCircle from "./PriorityCircle";
import { getStatusName, getTicketTypeName } from "../utils/Common";

const HomePage: React.FC = () => {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [page, setPage] = useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = useState<number>(5);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchTickets = async () => {
      setLoading(true);
      try {
        const data = await getTickets();
        setTickets(data);
      } catch (error) {
        console.error("Error fetching tickets:", error);
      }
      setLoading(false);
    };

    fetchTickets();
  }, []);

  const handleRowClick = (id: number) => {
    navigate(`/ticket/${id}`);
  };

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };
  
  const handleAddNewTicket = () => {
    console.log("Add new ticket");
  };

  return (
    <Box display="flex" flexDirection="column" minHeight="100vh">
      <Box display="flex" justifyContent="flex-end" width="100%">
      <Button variant="contained" color="primary" onClick={handleAddNewTicket} style={{ margin: '20px 20px 0 0' }} >
        Add New Ticket
      </Button>
    </Box>
  {loading ? (
    <CircularProgress />
  ) : (
    <TableContainer
      component={Paper}
      style={{ flexGrow: 1, overflowX: "auto" }}
    >
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Lvl</TableCell>
            <TableCell>#</TableCell>
            <TableCell>Title</TableCell>
            <TableCell>Module</TableCell>
            <TableCell>Type</TableCell>
            <TableCell>State</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {tickets
            .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
            .map((ticket) => (
              <TableRow
                hover
                key={ticket.id}
                onClick={() => handleRowClick(ticket.id)}
                style={{ cursor: "pointer" }}
              >
                <TableCell>
                      <PriorityCircle priorityId={ticket.priorityId} />
                    </TableCell>
                <TableCell>{ticket.id}</TableCell>
                <TableCell>{ticket.title}</TableCell>
                <TableCell>{ticket.applicationName}</TableCell>
                <TableCell>{getTicketTypeName(ticket.ticketTypeId)}</TableCell>
                <TableCell>{getStatusName(ticket.statusId)}</TableCell>
              </TableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  )}

  <Box
    display="flex"
    justifyContent="space-between"
    alignItems="center"
  >
    <TablePagination
      rowsPerPageOptions={[5, 10, 25]}
      component="div"
      count={tickets.length}
      rowsPerPage={rowsPerPage}
      page={page}
      onPageChange={handleChangePage}
      onRowsPerPageChange={handleChangeRowsPerPage}
    />
  </Box>
</Box>

  );
};

export default HomePage;
