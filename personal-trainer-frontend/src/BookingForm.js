import React, { useState } from 'react';
import axios from 'axios';

const BookingForm = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    service: '',
    date: '',
    time: ''
  });
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Validate form data
    if (!formData.name || !formData.email || !formData.service || !formData.date || !formData.time) {
      setErrorMessage('All fields are required.');
      return;
    }

    // Clear previous messages
    setSuccessMessage('');
    setErrorMessage('');

    // API logic here
    const bookingData = {
      name: formData.name,
      email: formData.email,
      service: formData.service,
      preferredDate: formData.date,
      preferredTime: formData.time,
    };

    try {
      const response = await axios.post('http://localhost:5000/bookings', bookingData);
      console.log('Booking submitted successfully', response.data);
      setSuccessMessage('Booking submitted successfully!');

      // Clear form fields after successful submission
      setFormData({
        name: '',
        email: '',
        service: '',
        date: '',
        time: ''
      });
    } catch (error) {
      console.error('There was an error submitting the booking!', error);
      setErrorMessage('Failed to submit booking. Please try again.');
    }
  };

  return (
    <div className="container mt-5">
      <h2>Book a Service</h2>
      {successMessage && <div className="alert alert-success">{successMessage}</div>}
      {errorMessage && <div className="alert alert-danger">{errorMessage}</div>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Name</label>
          <input 
            type="text" 
            name="name" 
            className="form-control" 
            value={formData.name} 
            onChange={handleChange} 
            required />
        </div>

        <div className="mb-3">
          <label className="form-label">Email</label>
          <input 
            type="email" 
            name="email" 
            className="form-control" 
            value={formData.email} 
            onChange={handleChange} 
            required />
        </div>

        <div className="mb-3">
          <label className="form-label">Service</label>
          <select 
            name="service" 
            className="form-select" 
            value={formData.service} 
            onChange={handleChange} 
            required>
            <option value="">Select a Service</option>
            <option value="Personal Training">Personal Training</option>
            <option value="Group Training">Group Training</option>
            <option value="Nutrition Consulting">Nutrition Consulting</option>
          </select>
        </div>

        <div className="mb-3">
          <label className="form-label">Preferred Date</label>
          <input 
            type="date" 
            name="date" 
            className="form-control" 
            value={formData.date} 
            onChange={handleChange} 
            required />
        </div>

        <div className="mb-3">
          <label className="form-label">Preferred Time</label>
          <input 
            type="time" 
            name="time" 
            className="form-control" 
            value={formData.time} 
            onChange={handleChange} 
            required />
        </div>

        <button type="submit" className="btn btn-primary">Submit</button>
      </form>
    </div>
  );
};

export default BookingForm;
