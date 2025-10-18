import { Link } from 'react-router-dom';
import './NavBar.css';

export default function NavBar() {
  return (
    <nav className="navbar">
      <div className="nav-container">
        <Link to="/" className="nav-brand">
          Hillary's Hair Care
        </Link>
        <ul className="nav-menu">
          <li className="nav-item">
            <Link to="/appointments" className="nav-link">
              Appointments
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/stylists" className="nav-link">
              Stylists
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/customers" className="nav-link">
              Customers
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/services" className="nav-link">
              Services
            </Link>
          </li>
        </ul>
      </div>
    </nav>
  );
}
