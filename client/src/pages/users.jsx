import React, { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { getRoles } from "../services/services.js";

export const Users = (props) => {
  const { user } = useAuth0();
  const [roles, setRole] = useState([]);
  const handleRoles = async () => {
    let userId = user.sub;
    console.log(userId);
    let roles = await getRoles(userId);
    console.log(roles[0].name);
    setRole(roles);
  };

  useEffect(() => {
    handleRoles();
  }, []);

  return roles;
};
