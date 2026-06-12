import { useState } from "react";
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
    <form onSubmit={handleSubmit}>
      <input value={name} onChange={(e) => setName(e.target.value)} placeholder="Name" />
      <input value={description} onChange={(e) => setDescription(e.target.value)} placeholder="Description" />
      <input value={price} onChange={(e) => setPrice(e.target.value)} placeholder="Price" type="number" />
      <button type="submit">Add Service</button>
    </form>
  );
};
