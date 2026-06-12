import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Table, Badge } from "react-bootstrap";
import { getAppointments } from "../data/appointments";

export const AppointmentList = () => {
  const [appointments, setAppointments] = useState([]);

  useEffect(() => {
    getAppointments().then(setAppointments);
  }, []);

  return (
    <div>
      <h2>Appointments</h2>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Customer</th>
            <th>Stylist</th>
            <th>Date & Time</th>
            <th>Total Cost</th>
            <th>Status</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {appointments.map((a) => (
            <tr
              key={a.id}
              style={a.isCancelled ? { opacity: 0.4, textDecoration: "line-through" } : {}}
            >
              <td>{a.customer.name}</td>
              <td>{a.stylist.name}</td>
              <td>
                {a.appointmentTime
                  ? new Date(a.appointmentTime).toLocaleString()
                  : "No time set"}
              </td>
              <td>${a.totalCost.toFixed(2)}</td>
              <td>
                {a.isCancelled ? (
                  <Badge bg="danger">Cancelled</Badge>
                ) : (
                  <Badge bg="success">Active</Badge>
                )}
              </td>
              <td>
                <Link to={`/appointments/${a.id}`}>View</Link>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};
