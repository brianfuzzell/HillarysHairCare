<!-- Last updated: 2026-06-11 -->
<!-- Last change: Step 2 updated to include React Bootstrap install and setup -->

# Hillary's Hair Care - Implementation Roadmap

Generated from: dev-docs/PRD.md

## Steps

- [x] **Step 1: Fix model and DTO bugs (prerequisites)**
  Three known issues in the current code must be resolved before any appointment endpoint can work correctly. Fixing them now avoids revisiting these files mid-feature.

  Changes:
  - `Models/Appointment.cs`: add `public List<AppointmentService> AppointmentServices { get; set; }` so EF Core can `.Include()` services on an appointment.
  - `Models/DTOs/AppointmentDTO.cs`: change `Customer` and `Stylist` property types from the raw model classes to `CustomerDTO` and `StylistDTO` to prevent circular reference errors during JSON serialization.
  - `Models/DTOs/AppointmentServiceDTO.cs`: change `Service` property type from the raw `Service` model to `ServiceDTO`, and remove the `Appointment` back-reference property entirely (it creates a nesting cycle and is not needed by any endpoint).

  **Acceptance Criteria:**
  - **Given** the three files are updated, **When** the project is built with `dotnet build`, **Then** it compiles with no errors or warnings about type mismatches.

- [x] **Step 2: Set up the React app shell**
  Before building any UI, `App.jsx` needs routes and a nav bar so each feature has a place to land. This is the last infrastructure step before feature work begins.

  Changes:
  - Run `npm install react-bootstrap bootstrap` from the `client/` directory to add React Bootstrap.
  - `client/src/main.jsx`: add `import 'bootstrap/dist/css/bootstrap.min.css'` so Bootstrap styles are available globally.
  - `client/src/App.jsx`: configure `react-router-dom` routes for `/`, `/appointments`, `/customers`, `/stylists`, and `/services`.
  - `client/src/components/NavBar.jsx`: create a nav bar using Bootstrap's `<Navbar>`, `<Nav>`, and `<Nav.Link>` components with links to each section.
  - Each route can render a placeholder `<p>Coming soon</p>` for now; components get filled in during each feature step.

  **Acceptance Criteria:**
  - **Given** both the .NET backend and Vite dev server are running, **When** the user clicks each nav link, **Then** the URL changes and the correct placeholder renders without a full page reload.

- [x] **Step 3: Services - view all and add new (Issues #11, #12)**
  Services are the simplest entity: no foreign keys, no soft-delete, and `ServiceDTO` is already correct. This step establishes the full-stack pattern (endpoint + data manager + component) before anything more complex.

  Changes:
  - `Program.cs`: add `GET /api/services` and `POST /api/services` endpoints.
  - `client/src/data/services.js`: implement `getServices()` and `createService()`.
  - `client/src/components/ServiceList.jsx`: list all services.
  - `client/src/components/AddServiceForm.jsx`: form to create a service.

  **Acceptance Criteria:**
  - **Given** the app is running, **When** Hillary navigates to `/services`, **Then** all seeded services are displayed.
  - **Given** the services list is visible, **When** Hillary submits the add-service form with a name, description, and price, **Then** the new service appears in the list without a page reload.

- [x] **Step 4: Stylists - view all and add new (Issues #8, #9)**
  Stylists introduce the `isActive` field. This step adds the read/create pattern for stylists and surfaces the active/inactive distinction in the UI (which deactivation in Step 5 will build on).

  Changes:
  - `Program.cs`: add `GET /api/stylists` and `POST /api/stylists` endpoints.
  - `client/src/data/stylists.js`: implement `getStylists()` and `createStylist()`.
  - `client/src/components/StylistList.jsx`: list all stylists, visually distinguishing active vs. inactive.
  - `client/src/components/AddStylistForm.jsx`: form to add a stylist.

  **Acceptance Criteria:**
  - **Given** the app is running, **When** Hillary navigates to `/stylists`, **Then** all seeded stylists are displayed with a visible indicator for inactive stylists.
  - **Given** the stylists list is visible, **When** Hillary submits the add-stylist form, **Then** the new stylist appears as active in the list.

- [ ] **Step 5: Deactivate a stylist (Issue #10)**
  This step introduces the soft-deactivate pattern: a `PUT` endpoint that updates a flag rather than deleting a record. It also teaches PUT endpoints that return `204 No Content` with no request body.

  Changes:
  - `Program.cs`: add `PUT /api/stylists/{id}/deactivate` endpoint (sets `isActive = false`, returns `NoContent()`).
  - `client/src/data/stylists.js`: implement `deactivateStylist(id)`.
  - `client/src/components/StylistList.jsx`: add a "Deactivate" button next to each active stylist; re-fetch or update local state on success.

  **Acceptance Criteria:**
  - **Given** an active stylist is visible in the list, **When** Hillary clicks "Deactivate" for that stylist, **Then** the stylist is immediately shown as inactive in the UI.
  - **Given** a stylist is deactivated, **When** the page is refreshed, **Then** the stylist still appears in the list but remains marked as inactive.

- [ ] **Step 6: Customers - view all and add new (Issues #6, #7)**
  Customers follow the same pattern as services and stylists. No foreign keys, no soft-delete. After this step, all three dropdown data sources (customers, active stylists, services) are available for the appointment creation form in Step 9.

  Changes:
  - `Program.cs`: add `GET /api/customers` and `POST /api/customers` endpoints.
  - `client/src/data/customers.js`: implement `getCustomers()` and `createCustomer()`.
  - `client/src/components/CustomerList.jsx`: list all customers.
  - `client/src/components/AddCustomerForm.jsx`: form to add a customer.

  **Acceptance Criteria:**
  - **Given** the app is running, **When** Hillary navigates to `/customers`, **Then** all seeded customers are displayed.
  - **Given** the customers list is visible, **When** Hillary submits the add-customer form, **Then** the new customer appears in the list.

- [ ] **Step 7: View all appointments (Issue #1)**
  This is the first endpoint that uses `Include` / `ThenInclude` to load related data and projects everything into a nested DTO. It requires the fixes from Step 1.

  The query must: `Include` Customer, `Include` Stylist, `Include` AppointmentServices then `ThenInclude` Service, and project into `AppointmentDTO` (including a computed `TotalCost` from `AppointmentServices.Sum(s => s.Service.Price)`).

  Changes:
  - `Program.cs`: add `GET /api/appointments`.
  - `client/src/data/appointments.js`: implement `getAppointments()`.
  - `client/src/components/AppointmentList.jsx`: list all appointments with customer name, stylist name, date/time, and total cost. Show cancelled appointments differently (e.g., greyed out or strikethrough).

  **Acceptance Criteria:**
  - **Given** the app is running, **When** Hillary navigates to `/appointments`, **Then** all seeded appointments are displayed with customer name, stylist name, date/time, and computed total cost.
  - **Given** an appointment is cancelled (from seed data), **When** it appears in the list, **Then** it is visually distinct from active appointments.

- [ ] **Step 8: View appointment details (Issue #2)**
  A single-record version of the appointments query. Same `Include` / `ThenInclude` chain, returns 404 if not found.

  Changes:
  - `Program.cs`: add `GET /api/appointments/{id}`.
  - `client/src/data/appointments.js`: implement `getAppointment(id)`.
  - `client/src/components/AppointmentDetail.jsx`: show full detail for one appointment, including the services list and total cost. Link to this from each row in `AppointmentList`.

  **Acceptance Criteria:**
  - **Given** the appointments list is visible, **When** Hillary clicks on an appointment, **Then** she is taken to a detail view that shows the customer, stylist, date/time, all services, and total cost.
  - **Given** Hillary navigates to `/appointments/9999` (a non-existent id), **When** the API is called, **Then** a 404 is returned and the UI shows a "not found" message.

- [ ] **Step 9: Create a new appointment (Issue #3)**
  The most complex form so far: the UI needs dropdowns for customer and stylist (re-using the list endpoints from Steps 4 and 6), and the API must enforce the business rule that only active stylists can be booked.

  Changes:
  - `Program.cs`: add `POST /api/appointments`. Before inserting, check that the selected stylist exists and `isActive == true`; return `Results.BadRequest()` if not.
  - `client/src/data/appointments.js`: implement `createAppointment(appointmentObj)`.
  - `client/src/components/AppointmentForm.jsx`: form with a customer dropdown, stylist dropdown (filtered to active only), date/time picker, and a service multi-select. On success, redirect to the appointment detail view.

  **Acceptance Criteria:**
  - **Given** the create-appointment form is open, **When** Hillary selects an active stylist, a customer, a date/time, and submits, **Then** a new appointment is created and she is redirected to the detail view for that appointment.
  - **Given** Hillary selects a stylist, **When** that stylist is inactive, **Then** that stylist does not appear in the dropdown at all (filtered on the frontend).

- [ ] **Step 10: Cancel an appointment (Issue #4)**
  Soft-cancel pattern: mirrors the deactivate-stylist endpoint from Step 5 but for appointments. Sets `IsCancelled = true`.

  Changes:
  - `Program.cs`: add `PUT /api/appointments/{id}/cancel`.
  - `client/src/data/appointments.js`: implement `cancelAppointment(id)`.
  - `client/src/components/AppointmentDetail.jsx`: add a "Cancel Appointment" button that only appears when `IsCancelled` is false. On success, re-fetch the appointment so the UI reflects the updated state.

  **Acceptance Criteria:**
  - **Given** an active appointment detail is displayed, **When** Hillary clicks "Cancel Appointment," **Then** the appointment is marked cancelled and the cancel button disappears.
  - **Given** a cancelled appointment detail is displayed, **When** the page loads, **Then** no cancel button is shown.

- [ ] **Step 11: Edit services on an appointment (Issue #5)**
  The most complex mutation in the project. The edit replaces the full set of `AppointmentService` join records for an appointment: delete all existing ones, then insert the new set. This step also resolves the `AppointmentServiceDTO` design question (it only needs `ServiceDTO`, no back-reference to `Appointment`).

  Changes:
  - `Program.cs`: add `PUT /api/appointments/{id}/services`. Accept `List<int>` of service IDs in the request body. Remove all existing `AppointmentService` rows for that appointment, then insert a new row for each service ID. Return `NoContent()`.
  - `client/src/data/appointments.js`: implement `updateAppointmentServices(id, serviceIds)`.
  - `client/src/components/EditServicesForm.jsx`: fetch all services, display as checkboxes with current services pre-checked, submit the updated list. Reachable from the appointment detail view.

  **Acceptance Criteria:**
  - **Given** an appointment detail is visible with services A and B, **When** Hillary opens the edit form, deselects A, adds C, and submits, **Then** the appointment now shows services B and C.
  - **Given** the edit form is submitted, **When** the user returns to the appointment detail, **Then** the total cost updates to reflect the new set of services.
