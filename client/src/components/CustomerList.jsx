import { useState, useEffect } from "react";
import { getCustomers } from "../data/customers";
import { AddCustomerForm } from "./AddCustomerForm";

export const CustomerList = () => {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    getCustomers().then(setCustomers);
  }, []);

  return (
    <div className="mt-4 px-3">
      <h2>Customers</h2>
      <div className="my-4">
        <AddCustomerForm setCustomers={setCustomers} />
      </div>
      <ul className="list-unstyled mt-4">
        {customers.map((c) => (
          <li key={c.id}>
            {c.name} &mdash; {c.email} &mdash; {c.phone}
          </li>
        ))}
      </ul>
    </div>
  );
};
