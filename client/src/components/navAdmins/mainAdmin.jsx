import React from "react";
import { Route, Routes } from "react-router-dom";
import { Admin } from "../../pages/admins/admin.admins";
import { ActivistTbl } from "../../pages/socialActivist/activistTbl";
import { TwitterInfo } from "../../pages/Twitter/twitterInfo";
import { NavAdmin } from "../navAdmins/navAdmin.jsx";

export const MainAdmin = () => {
  return (
    <div>
      <NavAdmin />
      <Routes>
        <Route path="/Admin" element={<Admin />} />
        <Route path="/activistTbl" element={<ActivistTbl />} />
        <Route path="/twitterTrack" element={<TwitterInfo />} />
      </Routes>
    </div>
  );
};
