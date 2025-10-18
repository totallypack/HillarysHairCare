import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

export default function AppointmentsList() {
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchAppointments();
  }, []);

  const fetchAppointments = async () => {
    try {
      const response = await fetch('/api/appointments');
      const data = await response.json();
      setAppointments(data);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching appointments:', error);
      setLoading(false);
    }
  };

  const handleCancelAppointment = async (id) => {
    if (!confirm('Are you sure you want to cancel this appointment?')) return;

    try {
      await fetch(`/api/appointments/${id}/cancel`, {
        method: 'PUT',
      });
      fetchAppointments(); // Refresh the list
    } catch (error) {
      console.error('Error canceling appointment:', error);
    }
  };

  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleString();
  };

  if (loading) return <div>Loading...</div>;

  return (
    <div>
      <h1>Appointments</h1>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Date</th>
              <th>Customer</th>
              <th>Stylist</th>
              <th>Services</th>
              <th>Total Cost</th>
              <th>Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {appointments.map((appointment) => (
              <tr key={appointment.id} className={appointment.isCanceled ? 'canceled' : ''}>
                <td>{formatDate(appointment.appointmentDate)}</td>
                <td>
                  {appointment.customer.firstName} {appointment.customer.lastName}
                </td>
                <td>
                  {appointment.stylist.firstName} {appointment.stylist.lastName}
                </td>
                <td>
                  {appointment.services?.map(s => s.name).join(', ') || 'None'}
                </td>
                <td>${appointment.totalCost?.toFixed(2)}</td>
                <td>
                  {appointment.isCanceled ? (
                    <span style={{ color: 'red' }}>Canceled</span>
                  ) : (
                    <span style={{ color: 'green' }}>Active</span>
                  )}
                </td>
                <td>
                  <Link to={`/appointments/${appointment.id}`}>
                    <button className="btn btn-primary">View</button>
                  </Link>
                  {!appointment.isCanceled && (
                    <button
                      className="btn btn-danger"
                      onClick={() => handleCancelAppointment(appointment.id)}
                    >
                      Cancel
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
