import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.bundle.min.js"
import "../../Assets/Styles/Sidebar.css";
import { Link } from "react-router-dom";


function Sidebar() {
  return (
    <>
      <div className="d-flex flex-column flex-shrink-0 m-0 p-3 text-bg-dark sidebar">
        <ul className="nav nav-pills flex-column mb-auto">
          <li className="nav-item">
            <a href="#" className="nav-link text-white font-weight-bold" aria-current="page">
              Dashboard
            </a>
          </li>
          <li>
            <button className="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed" data-bs-toggle="collapse" data-bs-target="#attorneys-collapse" aria-expanded="false">
              Attorneys
            </button>
            <div className="collapse bg-secondary rounded" id="attorneys-collapse">
              <ul className="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li><a href="#" className="link-body-emphasis d-inline-flex text-decoration-none rounded">Add Attorney</a></li>
                <li><a href="#" className="link-body-emphasis d-inline-flex text-decoration-none rounded">View Attorneys</a></li>
              </ul>
            </div>
          </li>
          <li>
            <button className="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed" data-bs-toggle="collapse" data-bs-target="#clients-collapse" aria-expanded="false">
              Clients
            </button>
            <div className="collapse bg-secondary rounded" id="clients-collapse">
              <ul className="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li><a href="#" className="link-body-emphasis d-inline-flex text-decoration-none rounded">Add client</a></li>
                <li><a href="#" className="link-body-emphasis d-inline-flex text-decoration-none rounded">View Clients</a></li>
              </ul>
            </div>
          </li>
          <li>
            <button className="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed" data-bs-toggle="collapse" data-bs-target="#matters-collapse" aria-expanded="false">
              Matters
            </button>
            <div className="collapse bg-secondary rounded" id="matters-collapse">
              <ul className="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li><Link to="/AddMatter" className="link-body-emphasis d-inline-flex text-decoration-none rounded">Create new Matter</Link></li>
                <li><Link to="/ViewMatters" className="link-body-emphasis d-inline-flex text-decoration-none rounded">View Matters</Link></li>
              </ul>
            </div>
          </li>
          <li>
            <button className="btn btn-toggle d-inline-flex align-items-center rounded border-0 collapsed" data-bs-toggle="collapse" data-bs-target="#invoices-collapse" aria-expanded="false">
              Invoices
            </button>
            <div className="collapse bg-secondary rounded" id="invoices-collapse">
              <ul className="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                <li><a href="#" className="link-body-emphasis d-inline-flex text-decoration-none rounded">Create new invoice</a></li>
                <li><Link to="/ViewInvoices" className="link-body-emphasis d-inline-flex text-decoration-none rounded">View invoices By Matter</Link></li>
                <li><Link to="/ViewInvoicesByAttorney" className="link-body-emphasis d-inline-flex text-decoration-none rounded">View invoices By Attorneys</Link></li>
              </ul>
            </div>
          </li>
        </ul>
        <hr />
        <div className="dropdown">
          <a href="#" className="d-flex align-items-center text-white text-decoration-none dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
            <img src="https://github.com/mdo.png" alt="" width="32" height="32" className="rounded-circle me-2" />
            <strong>mdo</strong>
          </a>
          <ul className="dropdown-menu dropdown-menu-dark text-small shadow">
            <li><a className="dropdown-item" href="#">New project...</a></li>
            <li><a className="dropdown-item" href="#">Settings</a></li>
            <li><a className="dropdown-item" href="#">Profile</a></li>
            <li><hr className="dropdown-divider" /></li>
            <li><a className="dropdown-item" href="#">Sign out</a></li>
          </ul>
        </div>
      </div>
    </>
  );
}

export default Sidebar;