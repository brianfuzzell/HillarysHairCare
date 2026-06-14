export const getCustomers = () =>
  fetch("/api/customers").then((r) => r.json());

export const createCustomer = (customerObj) =>
  fetch("/api/customers", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(customerObj),
  }).then((r) => r.json());
