import React, { useEffect, useState } from 'react';
import axios from 'axios';
import MatchCard from './MatchCard';
import { getMatchDetails } from '../services/apiUtilityService';

const Matches = () => {
    const [matches, setMatches] = useState([]);

    useEffect(() => {
        getMatchDetailsHandler();
      }, []);

      async function getMatchDetailsHandler() {
        let data = await getMatchDetails();
        if (data != null) {
          setMatches(data);
        }
        console.log(matches);
      }

      const displayMatches = () => {
        return Array.isArray(matches) && matches.length > 0 ? (
          matches.map((match) => (
            <MatchCard key={match.matchName} match={match} />
          ))
        ) : (
          <h1>No Matches Found</h1>
        );
      };

    return (
        <div className="container mt-5">
          {displayMatches()}
        </div>
      );
    };

export default Matches;


