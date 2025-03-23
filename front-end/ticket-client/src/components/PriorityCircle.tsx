import React from "react";

interface PriorityCircleProps {
  priorityId: number;
}

const PriorityCircle: React.FC<PriorityCircleProps> = ({ priorityId }) => {
  const getPriorityColor = (priorityId: number) => {
    switch (priorityId) {
      case 1:
        return "green";
      case 2:
        return "yellow";
      case 3:
        return "darkred";
      case 4:
        return "red";
      case 5:
        return "white"; 
      default:
        return "gray";
    }
  };

  const color = getPriorityColor(priorityId);

  return (
    <div
      style={{
        width: 20,
        height: 20,
        borderRadius: "50%",
        backgroundColor: color
      }}
    />
  );
};

export default PriorityCircle;
