export const getAppointments = () =>
  fetch("/api/appointments").then((r) => r.json());

export const getAppointment = (id) =>
  fetch(`/api/appointments/${id}`).then((r) => (r.ok ? r.json() : null));
