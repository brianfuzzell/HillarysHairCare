import { useState, useEffect } from "react";
import { getCustomers } from "../data/customers";
import { AddCustomerForm } from "./AddCustomerForm";

export const CustomerList = () => {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    getCustomers().then(setCustomers);
  }, []);

  return (
    <div>
      <h2>Customers</h2>
      <AddCustomerForm setCustomers={setCustomers} />
      <ul>
        {customers.map((c) => (
          <li key={c.id}>
            {c.name} &mdash; {c.email} &mdash; {c.phone}
          </li>
        ))}
      </ul>
    </div>
  );
};
