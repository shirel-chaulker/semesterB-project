import React from "react";
import { Link } from "react-router-dom";
import { LogoutButton } from "../../auth0/logout.button";
import "../../components/nav.css";

export const NavNonProfit = (props) => {
  return (
    <nav class="navbar navbar-dark bg-dark">
      <ul class="nav justify-content-center">
        <li class="nav-item">
          <Link className="nav-link" to="/signUpNonProfit">
            Sign up Non profit rep
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/NonProfit">
            Home page
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/campaignTbl">
            campaign table
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/createCamapign">
            Create camapaign
          </Link>
        </li>
        <li class="nav-item"></li>
        <li class="nav-item-btn">
          <LogoutButton />
        </li>
      </ul>
    </nav>
  );
};
