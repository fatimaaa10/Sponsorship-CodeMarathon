import React from 'react';

const MatchCard = ({ match }) => {
  return (
    <div className="card mb-3">
      <div className="card-body">
        <h5 className="card-title">Match Name: {match.matchName}</h5>
        <p className="card-text">Match Date: {match.matchDate}</p>
        <p className="card-text">Location: {match.location}</p>
        <p className="card-text">Total Payments: {match.totalPayments}</p>
      </div>
    </div>
  );
};

export default MatchCard;