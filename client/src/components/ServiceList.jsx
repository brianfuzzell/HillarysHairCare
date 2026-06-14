import { useState, useEffect } from "react";
import { getServices } from "../data/services";
import { AddServiceForm } from "./AddServiceForm";

export const ServiceList = () => {
  const [services, setServices] = useState([]);

  useEffect(() => {
    getServices().then(setServices);
  }, []);

  return (
    <div className="mt-4 px-3">
      <h2>Services</h2>
      <div className="my-4">
        <AddServiceForm setServices={setServices} />
      </div>
      <ul className="list-unstyled mt-4">
        {services.map((s) => (
          <li key={s.id}>
            {s.name} - {s.description} - ${s.price}
          </li>
        ))}
      </ul>
    </div>
  );
};
