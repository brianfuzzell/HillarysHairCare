import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { createService, getServices } from "../data/services";

export const AddServiceForm = ({ setServices }) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    createService({ name, description, price: parseFloat(price) }).then(() => {
      getServices().then(setServices);
      setName("");
      setDescription("");
      setPrice("");
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
        <Form.Label>Description</Form.Label>
        <Form.Control
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Description"
        />
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Price</Form.Label>
        <Form.Control
          value={price}
          onChange={(e) => setPrice(e.target.value)}
          placeholder="Price"
          type="number"
        />
      </Form.Group>
      <Button type="submit">Add Service</Button>
    </Form>
  );
};
