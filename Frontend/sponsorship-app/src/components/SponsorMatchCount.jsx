import React, {useState} from 'react';
import axios from 'axios';
import '../SponsorMatchCount.css';

const SponsorMatchCount = () => {
    const [year, setYear] = useState('');
    const [sponsors, setSponsors] = useState([]);
    const [error, setError] = useState('');

    const handleYearChange = (e) => {
        setYear(e.target.value);
    };

    const fetchSponsorMatchCounts = async (e) => {
        e.preventDefault();
        setError('');

        try {
            const response = await axios.get(`http://localhost:5179/api/Sponsorship/Sponsor Match Count`, 
                {params: {year}});
            setSponsors(response.data);
        } catch (error) {
            setError('Error fetching data');
        }
    }

    return (
        <div className="sponsor-match-count-container">
            <h2>Sponsor Match Count</h2>
            <form className="sponsor-match-count-form" onSubmit={fetchSponsorMatchCounts}>
                <label htmlFor='year'>Enter Year:</label>
                <input 
                    type='number'
                    id='year'
                    value={year}
                    onChange={handleYearChange}
                    required
                />
                <button type='submit'>Get Match Count</button>
            </form>

            {error && <p className='error-message'>{error}</p>}

            <table className="sponsor-table">
                <thead>
                    <tr>
                        <th>Sponsor Name</th>
                        <th>Match Count</th>
                    </tr>
                </thead>
                <tbody>
                    {sponsors.map((sponsor, index) => (
                        <tr key={index}>
                            <td>{sponsor.sponsorName}</td>
                            <td>{sponsor.matchCount}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default SponsorMatchCount;