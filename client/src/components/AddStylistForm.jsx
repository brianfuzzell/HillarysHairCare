import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { createStylist, getStylists } from "../data/stylists";

export const AddStylistForm = ({ setStylists }) => {
  const [name, setName] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    createStylist({ name }).then(() => {
      getStylists().then(setStylists);
      setName("");
    });
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group className="mb-3">
        <Form.Label>Name</Form.Label>
        <Form.Control
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Stylist name"
        />
      </Form.Group>
      <Button type="submit">Add Stylist</Button>
    </Form>
  );
};
