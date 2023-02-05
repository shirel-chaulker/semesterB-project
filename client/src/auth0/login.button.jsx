import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import "./login.css";

export const LoginButton = () => {
  //שלהם server מול ה HTTP ספרייה שעושה בקשות - useAuth0
  const { loginWithRedirect } = useAuth0();

  return (
    // לאיזה דף להחזיר את המשתמש - loginWithRedirect
    <div className="main">
      <div className="login">
        <h4>please connect to the system</h4>
        <button
          className="btn btn-primary"
          onClick={() => loginWithRedirect("http://localhost:3000")}
        >
          Log In
        </button>
      </div>
    </div>
  );
};
