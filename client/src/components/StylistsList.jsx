import { useState, useEffect } from 'react';

export default function StylistsList() {
  const [stylists, setStylists] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchStylists();
  }, []);

  const fetchStylists = async () => {
    try {
      const response = await fetch('/api/stylists');
      const data = await response.json();
      setStylists(data);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching stylists:', error);
      setLoading(false);
    }
  };

  const handleDeactivate = async (id) => {
    if (!confirm('Are you sure you want to deactivate this stylist?')) return;

    try {
      await fetch(`/api/stylists/${id}/deactivate`, {
        method: 'PUT',
      });
      fetchStylists();
    } catch (error) {
      console.error('Error deactivating stylist:', error);
    }
  };

  const handleReactivate = async (id) => {
    if (!confirm('Are you sure you want to reactivate this stylist?')) return;

    try {
      await fetch(`/api/stylists/${id}/reactivate`, {
        method: 'PUT',
      });
      fetchStylists();
    } catch (error) {
      console.error('Error reactivating stylist:', error);
    }
  };

  if (loading) return <div>Loading...</div>;

  return (
    <div>
      <h1>Stylists</h1>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {stylists.map((stylist) => (
              <tr key={stylist.id} className={!stylist.isActive ? 'inactive' : ''}>
                <td>
                  {stylist.firstName} {stylist.lastName}
                </td>
                <td>
                  {stylist.isActive ? (
                    <span style={{ color: 'green' }}>Active</span>
                  ) : (
                    <span style={{ color: 'red' }}>Inactive</span>
                  )}
                </td>
                <td>
                  {stylist.isActive ? (
                    <button
                      className="btn btn-danger"
                      onClick={() => handleDeactivate(stylist.id)}
                    >
                      Deactivate
                    </button>
                  ) : (
                    <button
                      className="btn btn-success"
                      onClick={() => handleReactivate(stylist.id)}
                    >
                      Reactivate
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
