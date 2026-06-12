export const getServices = () =>
  fetch("/api/services").then((r) => r.json());

export const createService = (serviceObj) =>
  fetch("/api/services", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(serviceObj),
  }).then((r) => r.json());
