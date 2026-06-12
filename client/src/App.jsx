import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { NavBar } from './components/NavBar'

function App() {
  return (
    <BrowserRouter>
      <NavBar />
      <Routes>
        <Route path="/" element={<p>Coming soon</p>} />
        <Route path="/appointments" element={<p>Coming soon</p>} />
        <Route path="/customers" element={<p>Coming soon</p>} />
        <Route path="/stylists" element={<p>Coming soon</p>} />
        <Route path="/services" element={<p>Coming soon</p>} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
