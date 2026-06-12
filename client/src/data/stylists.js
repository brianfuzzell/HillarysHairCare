export const getStylists = () =>
  fetch("/api/stylists").then((r) => r.json());

export const createStylist = (stylistObj) =>
  fetch("/api/stylists", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(stylistObj),
  }).then((r) => r.json());

export const deactivateStylist = (id) =>
  fetch(`/api/stylists/${id}/deactivate`, {
    method: "PUT",
  });
