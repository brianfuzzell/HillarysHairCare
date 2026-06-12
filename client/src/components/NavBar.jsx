import { Navbar, Nav, Container } from 'react-bootstrap'
import { Link } from 'react-router-dom'

export default function NavBar() {
  return (
    <Navbar bg="dark" variant="dark">
      <Container>
        <Navbar.Brand as={Link} to="/">Hillary's Hair Care</Navbar.Brand>
        <Nav>
          <Nav.Link as={Link} to="/appointments">Appointments</Nav.Link>
          <Nav.Link as={Link} to="/customers">Customers</Nav.Link>
          <Nav.Link as={Link} to="/stylists">Stylists</Nav.Link>
          <Nav.Link as={Link} to="/services">Services</Nav.Link>
        </Nav>
      </Container>
    </Navbar>
  )
}
