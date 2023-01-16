import React, { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { getRoles } from "../services/services.js";
import { MainAdmin } from "../components/navAdmins/mainAdmin.jsx";
import { LogoutButton } from "./logout.button.jsx";
import { MainNonProfit } from "../components/navNonProfit/mainNonProfit.jsx";
import { MainBC } from "../components/navBusinessCompany/mainBC.jsx";
import { MainSocialActivist } from "../components/navSocialActivist/mainSocialActivist.jsx";
import { LoginButton } from "./login.button.jsx";
import { ThemeContext } from "../context/theme.context.jsx";

export const Profile = () => {
  const [oneProduct, SetoneProduc] = useState("");
  const { user } = useAuth0();
  const [roles, setRole] = useState([]);
  const handleRoles = async () => {
    let userId = user.sub;
    console.log(userId);
    let roles = await getRoles(userId);
    console.log(roles[0].name);
    setRole(roles);
    SetoneProduc(roles[0].name);
  };

  useEffect(() => {
    handleRoles();
  }, []);

  return (
    <>
      <ThemeContext.Provider value={roles}>
        {roles.length > 0 ? (
          roles.map((n) => {
            if (n.name === "Admin") return <MainAdmin />;
            else if (n.name === "Non profit Org Rep") return <MainNonProfit />;
            else if (n.name === "Business Company Rep") return <MainBC />;
            else if (n.name === "Social activist")
              return <MainSocialActivist />;
            else
              return <h4>please sign up and wait for the admin approve you</h4>;
          })
        ) : (
          <>
            <h3>please Reconnect</h3>

            <LogoutButton />
          </>
        )}
      </ThemeContext.Provider>
    </>
  );
};
