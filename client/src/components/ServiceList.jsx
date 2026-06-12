import { useState, useEffect } from "react";
import { getServices } from "../data/services";
import { AddServiceForm } from "./AddServiceForm";

export const ServiceList = () => {
  const [services, setServices] = useState([]);

  useEffect(() => {
    getServices().then(setServices);
  }, []);

  return (
    <div>
      <h2>Services</h2>
      <AddServiceForm setServices={setServices} />
      <ul>
        {services.map((s) => (
          <li key={s.id}>
            {s.name} - {s.description} - ${s.price}
          </li>
        ))}
      </ul>
    </div>
  );
};
