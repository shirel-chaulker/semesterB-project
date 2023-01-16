import React from "react";
import { Link } from "react-router-dom";
import { LogoutButton } from "../../auth0/logout.button";
import "../../components/nav.css";

export const NavSocialActivist = (props) => {
  return (
    <nav class="navbar navbar-dark bg-dark">
      <ul class="nav justify-content-center">
        <li class="nav-item">
          <Link className="nav-link" to="/socialActivist">
            Social activist page
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/campaignTbl">
            campaign table
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/productsTbl">
            products table
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/purchasePage">
            purchase Page
          </Link>
        </li>
        <li class="nav-item-btn">
          <LogoutButton />
        </li>
      </ul>
    </nav>
  );
};
