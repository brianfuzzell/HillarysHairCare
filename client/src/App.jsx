import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { NavBar } from './components/NavBar'
import { ServiceList } from './components/ServiceList'

function App() {
  return (
    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path="/" element={<p>Coming soon</p>} />
        <Route path="/appointments" element={<p>Coming soon</p>} />
        <Route path="/customers" element={<p>Coming soon</p>} />
        <Route path="/stylists" element={<p>Coming soon</p>} />
        <Route path="/services" element={<ServiceList />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
