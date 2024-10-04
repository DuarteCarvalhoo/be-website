import React, { useState, useEffect } from 'react';
import axios from 'axios';

function ServicesPage() {
  const [services, setServices] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5000/services')
      .then(response => {
        setServices(response.data);
      })
      .catch(error => {
        console.error('Error fetching services:', error);
      });
  }, []);

  return (
    <div>
      <h2 className="mt-4">Our Services</h2>
      <ul className="list-group">
        {services.map((service) => (
          <li key={service.id} className="list-group-item d-flex justify-content-between align-items-center">
            <div>
              <h5>{service.name}</h5>
              <p>Price: ${service.price}</p>
            </div>
            <button className="btn btn-primary">Book Now</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ServicesPage;
