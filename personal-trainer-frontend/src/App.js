import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link} from 'react-router-dom';
import ServicesPage from './ServicesPage';
import HomePage from './HomePage';
import ContactPage from './ContactPage';
import BookingForm from './BookingForm';
import BookingHistory from './BookingHistory';
import AdminDashboard from './AdminDashboard';
import RegisterForm from './RegisterForm';
import LoginForm from './LoginForm';
import './styles.css';

function App() {
  return (
    <Router>
      <div className="container">
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="container-fluid">
          <Link className="navbar-brand" to="/">Personal Trainer</Link>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link className="nav-link" to="/services">Services</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/contact">Contact</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/booking">Book a Service</Link>
            </li>
          </ul>
          </div>
        </div>
      </nav>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/services" element={<ServicesPage />} />
          <Route path="/contact" element={<ContactPage />} />
          <Route path="/booking" element={<BookingForm />} />
          <Route path="/bookings" element={<AdminDashboard />} />
          <Route path="/bookings/:id" element={<BookingHistory />} />
          <Route path="/register" element={<RegisterForm />} />
          <Route path="/login" element={<LoginForm />} />
        </Routes>
      </div>
    </Router>
  )
}

export default App;