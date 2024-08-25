import React, { useEffect, useState } from 'react';
import { fetchSponsors } from '../services/apiUtilityService';

const SponsorList = () => {
    const [sponsors, setSponsors] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadSponsors = async () => {
            try {
              const data = await fetchSponsors();
              setSponsors(data);
            } catch (error) {
              setError('Failed to fetch sponsors');
            } finally {
              setLoading(false);
            }
        };
        loadSponsors();
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div className="sponsor-list">
      <h2>Sponsor List</h2>
      <ul>
        {sponsors.map((sponsor) => (
          <li key={sponsor.sponsorId}>
            <strong>{sponsor.sponsorName}</strong> - {sponsor.industryType} - {sponsor.contactEmail}
          </li>
        ))}
      </ul>
    </div>
  );

}

export default SponsorList;