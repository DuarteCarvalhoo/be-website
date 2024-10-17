import React, { useEffect, useState } from 'react';
import axios from 'axios';

const AdminDashboard = () => {
  const [bookings, setBookings] = useState([]);

  useEffect(() => {
    const fetchBookings = async () => {
      try {
        const response = await axios.get('http://localhost:5000/bookings');
        setBookings(response.data);
      } catch (error) {
        console.error('Error fetching bookings:', error);
      }
    };

    fetchBookings();
  }, []);

  return (
    <div>
      <h2>Admin Dashboard</h2>
      <ul>
        {bookings.map(booking => (
          <li key={booking.id}>
            {booking.id} - {booking.name} booked {booking.service} on {booking.preferredDate} at {booking.preferredTime}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AdminDashboard;