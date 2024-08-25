import axios from "axios";
const URL = 'http://localhost:5179/api/Sponsorship';

async function fetchSponsors () {
  let data = null;

  try {
    let response = await axios.get(`${URL}/Sponsors List`);
    if (response.status == 200) {
      data = await response.data;
      console.log(data);
    }
  } 
  catch (error) 
  {
    return JSON.stringify(error);
  }
  return data;
}


async function getMatchDetails() {
    let data = null;
  
    try {
      let response = await axios.get(`${URL}/Match Details`);
      if (response.status == 200) {
        data = await response.data;
        console.log(data);
      }
    } catch (error) {
      return JSON.stringify(error);
    }
    return data;
  }

  export const addPayment = async (payment) => {
    try {
      const response = await axios.post(`${URL}/Add Payment`, payment);
      return response.data;
    } catch (error) {
      console.error('Error adding payment:', error);
      throw error;
    }
  }

  export const getSponsorMatchCountByYear = async (year) => {
    try {
      const response = await axios.get(`${URL}/Sponsor Match Count`, { params: { year } });
      return response.data;
    } catch (error) {
      console.error('Error fetching sponsor match counts:', error);
      throw error;
    }
  }
  
export { fetchSponsors, getMatchDetails };
