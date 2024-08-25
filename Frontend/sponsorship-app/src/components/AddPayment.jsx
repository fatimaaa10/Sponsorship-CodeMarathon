import React, { useState } from "react";
import axios from "axios";

const AddPayment = () => {
    const [payment, setPayment] = useState({
        contractId: '',
        paymentDate: '',
        amountPaid: '',
        paymentStatus: ''
    })

    const [message, setMessage] = useState('');

    const handleChange = (e) => {
        const {name, value} = e.target;
        setPayment({
            ...payment,
            [name]: value,
        })
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!payment.contractId || !payment.paymentDate || !payment.amountPaid || !payment.paymentStatus) {
            setMessage('All fields are required');
            return;
        }
        try {
            const response = await axios.post('http://localhost:5179/api/Sponsorship/Add Payment', payment);
            if (response.status === 200) {
                setMessage('Payment added successfully');
            } else {
                setMessage('Failed to add payment');
            }
        } catch (error) {
            console.error('Error adding payment:', error);
            setMessage('Failed to add payment');
        }
    }

    return (
        <div className="payment-form-container">
            <h2>Add Payment</h2>
            <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Contract ID:</label>
          <input
            type="number"
            name="contractId"
            value={payment.contractId}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label>Payment Date:</label>
          <input
            type="date"
            name="paymentDate"
            value={payment.paymentDate}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label>Amount Paid:</label>
          <input
            type="number"
            name="amountPaid"
            value={payment.amountPaid}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label>Payment Status:</label>
          <input
            type="text"
            name="paymentStatus"
            value={payment.paymentStatus}
            onChange={handleChange}
            required
          />
        </div>
        <button className="form-button" type="submit">Add Payment</button>
        </form>
        {message && <p className="form-message">{message}</p>}
        </div>
    );
}

export default AddPayment;