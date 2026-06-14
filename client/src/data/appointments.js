export const getAppointments = () =>
  fetch("/api/appointments").then((r) => r.json());

export const getAppointment = (id) =>
  fetch(`/api/appointments/${id}`).then((r) => (r.ok ? r.json() : null));

export const createAppointment = (appointmentObj) =>
  fetch("/api/appointments", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(appointmentObj),
  }).then((r) => r.json());

export const cancelAppointment = (id) =>
  fetch(`/api/appointments/${id}/cancel`, { method: "PUT" });
