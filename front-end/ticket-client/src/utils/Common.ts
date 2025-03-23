export const getStatusName = (statusId: number): string => {
    switch (statusId) {
      case 1:
        return "New";
      case 2:
        return "Open";
      case 3:
        return "Awaiting Response - User";
      case 4:
        return "Awaiting Response - Development";
      case 5:
        return "Awaiting Response - Vendor";
      case 6:
        return "Closed";
      default:
        return "Unknown";
    }
  };

  export const getTicketTypeName = (ticketTypeId: number): string => {
    switch (ticketTypeId) {
      case 1:
        return "Question";
      case 2:
        return "Issue";
      case 3:
        return "Suggestion";
      case 4:
        return "Feedback";
      default:
        return "Unknown";
    }
  };