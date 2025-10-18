import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import NavBar from './components/NavBar';
import Home from './components/Home';
import AppointmentsList from './components/AppointmentsList';
import AppointmentDetails from './components/AppointmentDetails';
import StylistsList from './components/StylistsList';
import CustomersList from './components/CustomersList';
import ServicesList from './components/ServicesList';

function App() {
  return (
    <Router>
      <div className="App">
        <NavBar />
        <div className="container">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/appointments" element={<AppointmentsList />} />
            <Route path="/appointments/:id" element={<AppointmentDetails />} />
            <Route path="/stylists" element={<StylistsList />} />
            <Route path="/customers" element={<CustomersList />} />
            <Route path="/services" element={<ServicesList />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
