import { useState } from "react";
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
    <form onSubmit={handleSubmit}>
      <input
        value={name}
        onChange={(e) => setName(e.target.value)}
        placeholder="Stylist name"
      />
      <button type="submit">Add Stylist</button>
    </form>
  );
};
