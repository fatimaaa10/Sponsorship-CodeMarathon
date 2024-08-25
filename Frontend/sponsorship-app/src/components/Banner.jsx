import React from 'react';
import '../index.css';
import banner from '../assets/banner.jpg';

const Banner = () => {
  return (
    <div className="banner-container">
      <h1 className='banner-title'>Welcome to the IPL Sponsorship App</h1>
      <img className='banner-image' src={banner} alt="Banner" />
    </div>
  );
};

export default Banner;
