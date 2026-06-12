import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { createCustomer, getCustomers } from "../data/customers";

export const AddCustomerForm = ({ setCustomers }) => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    createCustomer({ name, email, phone }).then(() => {
      getCustomers().then(setCustomers);
      setName("");
      setEmail("");
      setPhone("");
    });
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group className="mb-3">
        <Form.Label>Name</Form.Label>
        <Form.Control
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Name"
        />
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Email</Form.Label>
        <Form.Control
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="Email"
          type="email"
        />
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Phone</Form.Label>
        <Form.Control
          value={phone}
          onChange={(e) => setPhone(e.target.value)}
          placeholder="Phone"
          type="tel"
        />
      </Form.Group>
      <Button type="submit">Add Customer</Button>
    </Form>
  );
};
