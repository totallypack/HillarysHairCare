import { Link } from 'react-router-dom';

export default function Home() {
  return (
    <div>
      <h1>Welcome to Hillary's Hair Care</h1>
      <p>Manage your salon appointments, stylists, customers, and services.</p>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(250px, 1fr))', gap: '1rem', marginTop: '2rem' }}>
        <Link to="/appointments" style={{ textDecoration: 'none' }}>
          <div className="card" style={{ cursor: 'pointer', textAlign: 'center' }}>
            <h2>Appointments</h2>
            <p>View and manage salon appointments</p>
          </div>
        </Link>

        <Link to="/stylists" style={{ textDecoration: 'none' }}>
          <div className="card" style={{ cursor: 'pointer', textAlign: 'center' }}>
            <h2>Stylists</h2>
            <p>Manage salon stylists</p>
          </div>
        </Link>

        <Link to="/customers" style={{ textDecoration: 'none' }}>
          <div className="card" style={{ cursor: 'pointer', textAlign: 'center' }}>
            <h2>Customers</h2>
            <p>View customer information</p>
          </div>
        </Link>

        <Link to="/services" style={{ textDecoration: 'none' }}>
          <div className="card" style={{ cursor: 'pointer', textAlign: 'center' }}>
            <h2>Services</h2>
            <p>View available services</p>
          </div>
        </Link>
      </div>
    </div>
  );
}
