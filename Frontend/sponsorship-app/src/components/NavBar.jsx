import React from "react";
import { Link } from "react-router-dom";
import '../index.css';

const NavBar = () => {
  return (
    <nav className="navbar">
      <Link to="/" className="nav-link">Home</Link>
      <Link to="/Sponsor-List" className="nav-link">Sponsor List</Link>
      <Link to="/Match-Details" className="nav-link">Match Details</Link>
      <Link to="/Sponsor-Match-Count" className="nav-link">Sponsor Match Count</Link>
      <Link to="/Add-Payment" className="nav-link">Add Payment</Link>
    </nav>
  );
};

export default NavBar;
