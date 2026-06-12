export const getAppointments = () =>
  fetch("/api/appointments").then((r) => r.json());
