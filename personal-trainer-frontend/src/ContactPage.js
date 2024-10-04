import React from 'react';

function ContactPage() {
  return (
    <div>
      <h2>Contact Us</h2>
      <form>
        <div className="mb-3">
          <label className="form-label">Name</label>
          <input type="text" className="form-control" required />
        </div>
        <div className="mb-3">
          <label className="form-label">Email</label>
          <input type="email" className="form-control" required />
        </div>
        <div className="mb-3">
          <label className="form-label">Message</label>
          <textarea className="form-control" required></textarea>
        </div>
        <button type="submit" className="btn btn-success">Send</button>
      </form>
    </div>
  );
}

export default ContactPage;