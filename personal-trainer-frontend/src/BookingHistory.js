import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const BookingHistory = () => {
    const { id } = useParams(); // Get the ID from the URL
    const [bookings, setBookings] = useState([]);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchBookings = async () => {
        try {
            const response = await axios.get(`http://localhost:5000/bookings/${id}`);
            setBookings(response.data);
        } catch (error) {
            console.error('Error fetching bookings:', error);
            setError('Could not fetch bookings. Please try again later.');
        }
        };

        fetchBookings();
    }, [id]);

    return (
        <div>
            <h2>Booking History for User ID: {id}</h2>
            {error && <div className="alert alert-danger">{error}</div>}
            <ul>
                {bookings.map(booking => (
                <li key={booking.id}>
                    {booking.service} on {new Date(booking.preferredDate).toLocaleDateString()} at {booking.preferredTime} from {booking.email}
                </li>
                ))}
            </ul>
        </div>
    );
};

export default BookingHistory;
