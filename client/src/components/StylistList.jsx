import { useState, useEffect } from "react";
import { Badge } from "react-bootstrap";
import { getStylists } from "../data/stylists";
import { AddStylistForm } from "./AddStylistForm";

export const StylistList = () => {
  const [stylists, setStylists] = useState([]);

  useEffect(() => {
    getStylists().then(setStylists);
  }, []);

  return (
    <div>
      <h2>Stylists</h2>
      <AddStylistForm setStylists={setStylists} />
      <ul>
        {stylists.map((s) => (
          <li key={s.id}>
            {s.name}{" "}
            {s.isActive ? (
              <Badge bg="success">Active</Badge>
            ) : (
              <Badge bg="secondary">Inactive</Badge>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};
