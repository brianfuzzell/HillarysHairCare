import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Card, ListGroup, Badge } from "react-bootstrap";
import { getAppointment } from "../data/appointments";

export const AppointmentDetail = () => {
  const { id } = useParams();
  const [appointment, setAppointment] = useState(undefined);

  useEffect(() => {
    getAppointment(id).then(setAppointment);
  }, [id]);

  if (appointment === undefined) return <p>Loading...</p>;
  if (appointment === null) return <p>Appointment not found.</p>;

  return (
    <Card style={{ maxWidth: 600, margin: "2rem auto" }}>
      <Card.Header className="d-flex justify-content-between align-items-center">
        <span>Appointment #{appointment.id}</span>
        {appointment.isCancelled ? (
          <Badge bg="danger">Cancelled</Badge>
        ) : (
          <Badge bg="success">Active</Badge>
        )}
      </Card.Header>
      <ListGroup variant="flush">
        <ListGroup.Item>
          <strong>Customer:</strong> {appointment.customer.name}
        </ListGroup.Item>
        <ListGroup.Item>
          <strong>Stylist:</strong> {appointment.stylist.name}
        </ListGroup.Item>
        <ListGroup.Item>
          <strong>Date &amp; Time:</strong>{" "}
          {appointment.appointmentTime
            ? new Date(appointment.appointmentTime).toLocaleString()
            : "No time set"}
        </ListGroup.Item>
        <ListGroup.Item>
          <strong>Services:</strong>
          <ul className="mt-1 mb-0">
            {appointment.appointmentServices.map((aps) => (
              <li key={aps.id}>
                {aps.service.name} — ${aps.service.price.toFixed(2)}
              </li>
            ))}
          </ul>
        </ListGroup.Item>
        <ListGroup.Item>
          <strong>Total Cost:</strong> ${appointment.totalCost.toFixed(2)}
        </ListGroup.Item>
      </ListGroup>
    </Card>
  );
};
