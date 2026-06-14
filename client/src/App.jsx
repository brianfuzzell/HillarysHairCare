import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { NavBar } from './components/NavBar'
import { ServiceList } from './components/ServiceList'
import { StylistList } from './components/StylistList'
import { CustomerList } from './components/CustomerList'
import { AppointmentList } from './components/AppointmentList'
import { AppointmentDetail } from './components/AppointmentDetail'
import { AppointmentForm } from './components/AppointmentForm'
import { EditServicesForm } from './components/EditServicesForm'

function App() {
  return (
    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path="/" element={<p>Coming soon</p>} />
        <Route path="/appointments" element={<AppointmentList />} />
        <Route path="/appointments/new" element={<AppointmentForm />} />
        <Route path="/appointments/:id" element={<AppointmentDetail />} />
        <Route path="/appointments/:id/edit-services" element={<EditServicesForm />} />
        <Route path="/customers" element={<CustomerList />} />
        <Route path="/stylists" element={<StylistList />} />
        <Route path="/services" element={<ServiceList />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
