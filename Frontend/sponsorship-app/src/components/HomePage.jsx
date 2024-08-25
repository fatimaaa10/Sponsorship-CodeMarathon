import React from "react";
import Banner from "./Banner";
import '../index.css';

const Home = () => {
  return (
    <div className="home-container">
      <Banner />
      <p>This application provides detailed insights into IPL sponsorships.</p>
    </div>
  );
};

export default Home;
