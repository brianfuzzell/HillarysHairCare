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
    <div className="mt-4 px-3">
      <h2>Stylists</h2>
      <div className="my-4">
        <AddStylistForm setStylists={setStylists} />
      </div>
      <ul className="list-unstyled mt-4">
        {stylists.map((s) => (
          <li key={s.id} className="mb-3 d-flex align-items-center justify-content-center gap-2">
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
