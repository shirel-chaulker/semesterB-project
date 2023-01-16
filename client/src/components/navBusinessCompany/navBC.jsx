import React from "react";
import { Link } from "react-router-dom";
import { LogoutButton } from "../../auth0/logout.button";
import "../../components/nav.css";

export const NavBC = () => {
  return (
    <nav class="navbar navbar-dark bg-dark">
      <ul class="nav justify-content-center">
        <li class="nav-item">
          <Link className="nav-link" to="/businessCompany">
            Business company
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/campaignTbl">
            campaign table
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/productsTbl">
            product table
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/donationPage">
            donation page
          </Link>
        </li>

        <li class="nav-item-btn">
          <LogoutButton />
        </li>
      </ul>
    </nav>
  );
};
