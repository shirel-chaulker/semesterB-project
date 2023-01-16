import React from "react";
import { Link } from "react-router-dom";
import { LogoutButton } from "../../auth0/logout.button";
import "../../components/nav.css";

export const NavAdmin = (props) => {
  return (
    <nav class="navbar navbar-dark bg-dark">
      <ul class="nav justify-content-center">
        <li class="nav-item">
          <Link className="nav-link" to="/Admin">
            Admin page
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/activistTbl">
            Activists page
          </Link>
        </li>
        <li class="nav-item">
          <Link className="nav-link" to="/twitterTrack">
            Twitter track
          </Link>
        </li>
        <li class="nav-item-btn">
          <LogoutButton />
        </li>
      </ul>
    </nav>
  );
};
