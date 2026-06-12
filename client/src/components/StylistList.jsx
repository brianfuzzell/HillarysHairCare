import { useState, useEffect } from "react";
import { Badge, Button } from "react-bootstrap";
import { getStylists, deactivateStylist } from "../data/stylists";
import { AddStylistForm } from "./AddStylistForm";

export const StylistList = () => {
  const [stylists, setStylists] = useState([]);

  useEffect(() => {
    getStylists().then(setStylists);
  }, []);

  const handleDeactivate = (id) => {
    deactivateStylist(id).then(() => {
      getStylists().then(setStylists);
    });
  };

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
            {s.isActive && (
              <Button
                variant="outline-danger"
                size="sm"
                className="ms-2"
                onClick={() => handleDeactivate(s.id)}
              >
                Deactivate
              </Button>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};
