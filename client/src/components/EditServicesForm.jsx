import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Card, Form, Button } from "react-bootstrap";
import { getAppointment, updateAppointmentServices } from "../data/appointments";
import { getServices } from "../data/services";

export const EditServicesForm = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [allServices, setAllServices] = useState([]);
  const [selectedServiceIds, setSelectedServiceIds] = useState(null);

  useEffect(() => {
    const loadData = async () => {
      const appointment = await getAppointment(id);
      const services = await getServices();
      setSelectedServiceIds(
        appointment.appointmentServices.map((aps) => aps.serviceId)
      );
      setAllServices(services);
    };
    loadData();
  }, [id]);

  const handleToggle = (serviceId) => {
    if (selectedServiceIds.includes(serviceId)) {
      setSelectedServiceIds(selectedServiceIds.filter((sid) => sid !== serviceId));
    } else {
      setSelectedServiceIds([...selectedServiceIds, serviceId]);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    updateAppointmentServices(id, selectedServiceIds).then(() =>
      navigate(`/appointments/${id}`)
    );
  };

  if (selectedServiceIds === null) return <p>Loading...</p>;

  return (
    <Card style={{ maxWidth: 500, margin: "2rem auto" }}>
      <Card.Header>Edit Services</Card.Header>
      <Card.Body>
        <Form onSubmit={handleSubmit}>
          {allServices.map((service) => (
            <Form.Check
              key={service.id}
              type="checkbox"
              id={`service-${service.id}`}
              label={`${service.name} — $${service.price.toFixed(2)}`}
              checked={selectedServiceIds.includes(service.id)}
              onChange={() => handleToggle(service.id)}
            />
          ))}
          <Button type="submit" className="mt-3">
            Save Changes
          </Button>
        </Form>
      </Card.Body>
    </Card>
  );
};
