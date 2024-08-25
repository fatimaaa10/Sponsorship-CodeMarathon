import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "../components/HomePage";
import NavBar from "../components/NavBar";
import Matches from "../components/MatchDetails";
import SponsorList from "../components/SponsorList";
import AddPayment from "../components/AddPayment";
import SponsorMatchCount from "../components/SponsorMatchCount";

const RouterConfig = () => {
  return (
    
    <Router>
      <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Sponsor-List" element={<SponsorList />} />
          <Route path="/Match-Details" element={<Matches />} />
          <Route path="/Sponsor-Match-Count" element={<SponsorMatchCount />} />
          <Route path="/Add-Payment" element={<AddPayment />} />
        </Routes>
    </Router>
    
  );
};

export default RouterConfig;
