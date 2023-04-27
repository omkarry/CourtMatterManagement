import { AppContext } from "../../App";
import React from 'react'
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.bundle.min.js"

function Header() {
  const { profile } = React.useContext(AppContext);

  return (
    <header className="p-3 border-bottom">
      <div className="container">
        <div className="d-flex flex-wrap align-items-center justify-content-between justify-content-lg-between">
          <a href="/" className="col-md-4 d-flex align-items-center mb-2 mb-lg-0 link-body-emphasis text-decoration-none">
            <h3>Lexicon</h3>
          </a>

          <form className="col-md-6 mb-3 mb-lg-0 me-lg-3" role="search">
            <input type="search" className="form-control" placeholder="Search..." aria-label="Search" />
          </form>

          <div className="dropdown text-end">
            <a href="#" className="d-block link-dark text-decoration-none dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
              <img src="https://github.com/mdo.png" alt="mdo" width="32" height="32" className="rounded-circle" />
            </a>
            <ul className="dropdown-menu text-small">
              <li><a className="dropdown-item" href="#">Settings</a></li>
              <li><a className="dropdown-item" href="#">Profile</a></li>
              <li><a className="dropdown-item" href="#">Sign out</a></li>
            </ul>
          </div>
        </div>
      </div>
    </header>
  );
}

export default Header