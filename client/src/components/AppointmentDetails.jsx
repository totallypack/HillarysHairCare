import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

export default function AppointmentDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [appointment, setAppointment] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchAppointment();
  }, [id]);

  const fetchAppointment = async () => {
    try {
      const response = await fetch(`/api/appointments/${id}`);
      if (response.ok) {
        const data = await response.json();
        setAppointment(data);
      }
      setLoading(false);
    } catch (error) {
      console.error('Error fetching appointment:', error);
      setLoading(false);
    }
  };

  const handleCancel = async () => {
    if (!confirm('Are you sure you want to cancel this appointment?')) return;

    try {
      await fetch(`/api/appointments/${id}/cancel`, {
        method: 'PUT',
      });
      fetchAppointment();
    } catch (error) {
      console.error('Error canceling appointment:', error);
    }
  };

  const handleDelete = async () => {
    if (!confirm('Are you sure you want to permanently delete this appointment?')) return;

    try {
      await fetch(`/api/appointments/${id}`, {
        method: 'DELETE',
      });
      navigate('/appointments');
    } catch (error) {
      console.error('Error deleting appointment:', error);
    }
  };

  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleString();
  };

  if (loading) return <div>Loading...</div>;
  if (!appointment) return <div>Appointment not found</div>;

  return (
    <div>
      <h1>Appointment Details</h1>

      <div className="card">
        <h2>
          {appointment.isCanceled && (
            <span style={{ color: 'red' }}>[CANCELED] </span>
          )}
          Appointment #{appointment.id}
        </h2>

        <div style={{ marginTop: '1rem' }}>
          <p><strong>Date:</strong> {formatDate(appointment.appointmentDate)}</p>
          <p><strong>Customer:</strong> {appointment.customer.firstName} {appointment.customer.lastName}</p>
          <p><strong>Email:</strong> {appointment.customer.email}</p>
          <p><strong>Phone:</strong> {appointment.customer.phoneNumber}</p>
        </div>

        <div style={{ marginTop: '1rem' }}>
          <p><strong>Stylist:</strong> {appointment.stylist.firstName} {appointment.stylist.lastName}</p>
          <p><strong>Stylist Status:</strong> {appointment.stylist.isActive ? 'Active' : 'Inactive'}</p>
        </div>

        <div style={{ marginTop: '1rem' }}>
          <h3>Services</h3>
          {appointment.services && appointment.services.length > 0 ? (
            <ul>
              {appointment.services.map((service) => (
                <li key={service.id}>
                  {service.name} - ${service.price.toFixed(2)}
                  <br />
                  <small>{service.description}</small>
                </li>
              ))}
            </ul>
          ) : (
            <p>No services added yet</p>
          )}
          <p><strong>Total Cost: ${appointment.totalCost?.toFixed(2)}</strong></p>
        </div>

        <div style={{ marginTop: '2rem' }}>
          <button className="btn btn-secondary" onClick={() => navigate('/appointments')}>
            Back to List
          </button>
          {!appointment.isCanceled && (
            <button className="btn btn-danger" onClick={handleCancel}>
              Cancel Appointment
            </button>
          )}
          <button className="btn btn-danger" onClick={handleDelete}>
            Delete Appointment
          </button>
        </div>
      </div>
    </div>
  );
}
