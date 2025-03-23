import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import HomePage from "./components/HomePage";
import TicketDetail from "./components/TicketDetail";

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/ticket/:id" element={<TicketDetail />} />
      </Routes>
    </Router>
  );
};

export default App;
