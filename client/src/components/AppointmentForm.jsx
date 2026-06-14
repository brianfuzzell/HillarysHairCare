import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button, Card } from "react-bootstrap";
import { createAppointment } from "../data/appointments";
import { getCustomers } from "../data/customers";
import { getStylists } from "../data/stylists";
import { getServices } from "../data/services";

export const AppointmentForm = () => {
  const navigate = useNavigate();
  const [customers, setCustomers] = useState([]);
  const [stylists, setStylists] = useState([]);
  const [services, setServices] = useState([]);

  const [customerId, setCustomerId] = useState("");
  const [stylistId, setStylistId] = useState("");
  const [appointmentTime, setAppointmentTime] = useState("");
  const [selectedServiceIds, setSelectedServiceIds] = useState([]);

  useEffect(() => {
    getCustomers().then(setCustomers);
    getStylists().then((allStylists) => setStylists(allStylists.filter((s) => s.isActive)));
    getServices().then(setServices);
  }, []);

  const handleServiceToggle = (id) => {
    if (selectedServiceIds.includes(id)) {
      setSelectedServiceIds(selectedServiceIds.filter((serviceId) => serviceId !== id));
    } else {
      setSelectedServiceIds([...selectedServiceIds, id]);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    createAppointment({
      customerId: parseInt(customerId),
      stylistId: parseInt(stylistId),
      appointmentTime: appointmentTime || null,
      serviceIds: selectedServiceIds,
    }).then((newAppointment) => {
      navigate(`/appointments/${newAppointment.id}`);
    });
  };

  return (
    <Card style={{ maxWidth: 600, margin: "2rem auto" }}>
      <Card.Header>New Appointment</Card.Header>
      <Card.Body>
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Customer</Form.Label>
            <Form.Select
              value={customerId}
              onChange={(e) => setCustomerId(e.target.value)}
              required
            >
              <option value="">Select a customer...</option>
              {customers.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.name}
                </option>
              ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Stylist</Form.Label>
            <Form.Select
              value={stylistId}
              onChange={(e) => setStylistId(e.target.value)}
              required
            >
              <option value="">Select a stylist...</option>
              {stylists.map((s) => (
                <option key={s.id} value={s.id}>
                  {s.name}
                </option>
              ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Date &amp; Time</Form.Label>
            <Form.Control
              type="datetime-local"
              value={appointmentTime}
              onChange={(e) => setAppointmentTime(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Services</Form.Label>
            {services.map((s) => (
              <Form.Check
                key={s.id}
                type="checkbox"
                label={`${s.name} — $${s.price.toFixed(2)}`}
                checked={selectedServiceIds.includes(s.id)}
                onChange={() => handleServiceToggle(s.id)}
              />
            ))}
          </Form.Group>

          <Button type="submit">Create Appointment</Button>
        </Form>
      </Card.Body>
    </Card>
  );
};
